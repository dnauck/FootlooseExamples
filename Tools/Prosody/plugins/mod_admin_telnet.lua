-- Prosody IM
-- Copyright (C) 2008-2010 Matthew Wild
-- Copyright (C) 2008-2010 Waqas Hussain
-- 
-- This project is MIT/X11 licensed. Please see the
-- COPYING file in the source package for more information.
--

module.host = "*";

local _G = _G;

local prosody = _G.prosody;
local hosts = prosody.hosts;
local connlisteners_register = require "net.connlisteners".register;

local console_listener = { default_port = 5582; default_mode = "*l"; default_interface = "127.0.0.1" };

require "util.iterators";
local jid_bare = require "util.jid".bare;
local set, array = require "util.set", require "util.array";

local commands = {};
local def_env = {};
local default_env_mt = { __index = def_env };

prosody.console = { commands = commands, env = def_env };

local function redirect_output(_G, session)
	local env = setmetatable({ print = session.print }, { __index = function (t, k) return rawget(_G, k); end });
	env.dofile = function(name)
		local f, err = loadfile(name);
		if not f then return f, err; end
		return setfenv(f, env)();
	end;
	return env;
end

console = {};

function console:new_session(conn)
	local w = function(s) conn:write(s:gsub("\n", "\r\n")); end;
	local session = { conn = conn;
			send = function (t) w(tostring(t)); end;
			print = function (...)
				local t = {};
				for i=1,select("#", ...) do
					t[i] = tostring(select(i, ...));
				end
				w("| "..table.concat(t, "\t").."\n");
			end;
			disconnect = function () conn:close(); end;
			};
	session.env = setmetatable({}, default_env_mt);
	
	-- Load up environment with helper objects
	for name, t in pairs(def_env) do
		if type(t) == "table" then
			session.env[name] = setmetatable({ session = session }, { __index = t });
		end
	end
	
	return session;
end

local sessions = {};

function console_listener.onconnect(conn)
	-- Handle new connection
	local session = console:new_session(conn);
	sessions[conn] = session;
	printbanner(session);
	session.send(string.char(0));
end

function console_listener.onincoming(conn, data)
	local session = sessions[conn];

	-- Handle data
	(function(session, data)
		local useglobalenv;
		
		if data:match("^>") then
			data = data:gsub("^>", "");
			useglobalenv = true;
		elseif data == "\004" then
			commands["bye"](session, data);
			return;
		else
			local command = data:lower();
			command = data:match("^%w+") or data:match("%p");
			if commands[command] then
				commands[command](session, data);
				return;
			end
		end

		session.env._ = data;
		
		local chunkname = "=console";
		local chunk, err = loadstring("return "..data, chunkname);
		if not chunk then
			chunk, err = loadstring(data, chunkname);
			if not chunk then
				err = err:gsub("^%[string .-%]:%d+: ", "");
				err = err:gsub("^:%d+: ", "");
				err = err:gsub("'<eof>'", "the end of the line");
				session.print("Sorry, I couldn't understand that... "..err);
				return;
			end
		end
		
		setfenv(chunk, (useglobalenv and redirect_output(_G, session)) or session.env or nil);
		
		local ranok, taskok, message = pcall(chunk);
		
		if not (ranok or message or useglobalenv) and commands[data:lower()] then
			commands[data:lower()](session, data);
			return;
		end
		
		if not ranok then
			session.print("Fatal error while running command, it did not complete");
			session.print("Error: "..taskok);
			return;
		end
		
		if not message then
			session.print("Result: "..tostring(taskok));
			return;
		elseif (not taskok) and message then
			session.print("Command completed with a problem");
			session.print("Message: "..tostring(message));
			return;
		end
		
		session.print("OK: "..tostring(message));
	end)(session, data);
	
	session.send(string.char(0));
end

function console_listener.ondisconnect(conn, err)
	local session = sessions[conn];
	if session then
		session.disconnect();
		sessions[conn] = nil;
	end
end

connlisteners_register('console', console_listener);

-- Console commands --
-- These are simple commands, not valid standalone in Lua

function commands.bye(session)
	session.print("See you! :)");
	session.disconnect();
end
commands.quit, commands.exit = commands.bye, commands.bye;

commands["!"] = function (session, data)
	if data:match("^!!") and session.env._ then
		session.print("!> "..session.env._);
		return console_listener.onincoming(session.conn, session.env._);
	end
	local old, new = data:match("^!(.-[^\\])!(.-)!$");
	if old and new then
		local ok, res = pcall(string.gsub, session.env._, old, new);
		if not ok then
			session.print(res)
			return;
		end
		session.print("!> "..res);
		return console_listener.onincoming(session.conn, res);
	end
	session.print("Sorry, not sure what you want");
end


function commands.help(session, data)
	local print = session.print;
	local section = data:match("^help (%w+)");
	if not section then
		print [[Commands are divided into multiple sections. For help on a particular section, ]]
		print [[type: help SECTION (for example, 'help c2s'). Sections are: ]]
		print [[]]
		print [[c2s - Commands to manage local client-to-server sessions]]
		print [[s2s - Commands to manage sessions between this server and others]]
		print [[module - Commands to load/reload/unload modules/plugins]]
		print [[host - Commands to activate, deactivate and list virtual hosts]]
		print [[server - Uptime, version, shutting down, etc.]]
		print [[config - Reloading the configuration, etc.]]
		print [[console - Help regarding the console itself]]
	elseif section == "c2s" then
		print [[c2s:show(jid) - Show all client sessions with the specified JID (or all if no JID given)]]
		print [[c2s:show_insecure() - Show all unencrypted client connections]]
		print [[c2s:show_secure() - Show all encrypted client connections]]
		print [[c2s:close(jid) - Close all sessions for the specified JID]]
	elseif section == "s2s" then
		print [[s2s:show(domain) - Show all s2s connections for the given domain (or all if no domain given)]]
		print [[s2s:close(from, to) - Close a connection from one domain to another]]
	elseif section == "module" then
		print [[module:load(module, host) - Load the specified module on the specified host (or all hosts if none given)]]
		print [[module:reload(module, host) - The same, but unloads and loads the module (saving state if the module supports it)]]
		print [[module:unload(module, host) - The same, but just unloads the module from memory]]
		print [[module:list(host) - List the modules loaded on the specified host]]
	elseif section == "host" then
		print [[host:activate(hostname) - Activates the specified host]]
		print [[host:deactivate(hostname) - Disconnects all clients on this host and deactivates]]
		print [[host:list() - List the currently-activated hosts]]
	elseif section == "server" then
		print [[server:version() - Show the server's version number]]
		print [[server:uptime() - Show how long the server has been running]]
		print [[server:shutdown(reason) - Shut down the server, with an optional reason to be broadcast to all connections]]
	elseif section == "config" then
		print [[config:reload() - Reload the server configuration. Modules may need to be reloaded for changes to take effect.]]
	elseif section == "console" then
		print [[Hey! Welcome to Prosody's admin console.]]
		print [[First thing, if you're ever wondering how to get out, simply type 'quit'.]]
		print [[Secondly, note that we don't support the full telnet protocol yet (it's coming)]]
		print [[so you may have trouble using the arrow keys, etc. depending on your system.]]
		print [[]]
		print [[For now we offer a couple of handy shortcuts:]]
		print [[!! - Repeat the last command]]
		print [[!old!new! - repeat the last command, but with 'old' replaced by 'new']]
		print [[]]
		print [[For those well-versed in Prosody's internals, or taking instruction from those who are,]]
		print [[you can prefix a command with > to escape the console sandbox, and access everything in]]
		print [[the running server. Great fun, but be careful not to break anything :)]]
	end
	print [[]]
end

-- Session environment --
-- Anything in def_env will be accessible within the session as a global variable

def_env.server = {};

function def_env.server:insane_reload()
	prosody.unlock_globals();
	dofile "prosody"
	prosody = _G.prosody;
	return true, "Server reloaded";
end

function def_env.server:version()
	return true, tostring(prosody.version or "unknown");
end

function def_env.server:uptime()
	local t = os.time()-prosody.start_time;
	local seconds = t%60;
	t = (t - seconds)/60;
	local minutes = t%60;
	t = (t - minutes)/60;
	local hours = t%24;
	t = (t - hours)/24;
	local days = t;
	return true, string.format("This server has been running for %d day%s, %d hour%s and %d minute%s (since %s)",
		days, (days ~= 1 and "s") or "", hours, (hours ~= 1 and "s") or "",
		minutes, (minutes ~= 1 and "s") or "", os.date("%c", prosody.start_time));
end

function def_env.server:shutdown(reason)
	prosody.shutdown(reason);
	return true, "Shutdown initiated";
end

def_env.module = {};

local function get_hosts_set(hosts, module)
	if type(hosts) == "table" then
		if hosts[1] then
			return set.new(hosts);
		elseif hosts._items then
			return hosts;
		end
	elseif type(hosts) == "string" then
		return set.new { hosts };
	elseif hosts == nil then
		local mm = require "modulemanager";
		return set.new(array.collect(keys(prosody.hosts)))
			/ function (host) return prosody.hosts[host].type == "local" or module and mm.is_loaded(host, module); end;
	end
end

function def_env.module:load(name, hosts, config)
	local mm = require "modulemanager";
	
	hosts = get_hosts_set(hosts);
	
	-- Load the module for each host
	local ok, err, count = true, nil, 0;
	for host in hosts do
		if (not mm.is_loaded(host, name)) then
			ok, err = mm.load(host, name, config);
			if not ok then
				ok = false;
				self.session.print(err or "Unknown error loading module");
			else
				count = count + 1;
				self.session.print("Loaded for "..host);
			end
		end
	end
	
	return ok, (ok and "Module loaded onto "..count.." host"..(count ~= 1 and "s" or "")) or ("Last error: "..tostring(err));	
end

function def_env.module:unload(name, hosts)
	local mm = require "modulemanager";

	hosts = get_hosts_set(hosts, name);
	
	-- Unload the module for each host
	local ok, err, count = true, nil, 0;
	for host in hosts do
		if mm.is_loaded(host, name) then
			ok, err = mm.unload(host, name);
			if not ok then
				ok = false;
				self.session.print(err or "Unknown error unloading module");
			else
				count = count + 1;
				self.session.print("Unloaded from "..host);
			end
		end
	end
	return ok, (ok and "Module unloaded from "..count.." host"..(count ~= 1 and "s" or "")) or ("Last error: "..tostring(err));
end

function def_env.module:reload(name, hosts)
	local mm = require "modulemanager";

	hosts = get_hosts_set(hosts, name);
	
	-- Reload the module for each host
	local ok, err, count = true, nil, 0;
	for host in hosts do
		if mm.is_loaded(host, name) then
			ok, err = mm.reload(host, name);
			if not ok then
				ok = false;
				self.session.print(err or "Unknown error reloading module");
			else
				count = count + 1;
				if ok == nil then
					ok = true;
				end
				self.session.print("Reloaded on "..host);
			end
		end
	end
	return ok, (ok and "Module reloaded on "..count.." host"..(count ~= 1 and "s" or "")) or ("Last error: "..tostring(err));
end

function def_env.module:list(hosts)
	if hosts == nil then
		hosts = array.collect(keys(prosody.hosts));
	end
	if type(hosts) == "string" then
		hosts = { hosts };
	end
	if type(hosts) ~= "table" then
		return false, "Please supply a host or a list of hosts you would like to see";
	end
	
	local print = self.session.print;
	for _, host in ipairs(hosts) do
		print(host..":");
		local modules = array.collect(keys(prosody.hosts[host] and prosody.hosts[host].modules or {})):sort();
		if #modules == 0 then
			if prosody.hosts[host] then
				print("    No modules loaded");
			else
				print("    Host not found");
			end
		else
			for _, name in ipairs(modules) do
				print("    "..name);
			end
		end
	end
end

def_env.config = {};
function def_env.config:load(filename, format)
	local config_load = require "core.configmanager".load;
	local ok, err = config_load(filename, format);
	if not ok then
		return false, err or "Unknown error loading config";
	end
	return true, "Config loaded";
end

function def_env.config:get(host, section, key)
	local config_get = require "core.configmanager".get
	return true, tostring(config_get(host, section, key));
end

function def_env.config:reload()
	local ok, err = prosody.reload_config();
	return ok, (ok and "Config reloaded (you may need to reload modules to take effect)") or tostring(err);
end

def_env.hosts = {};
function def_env.hosts:list()
	for host, host_session in pairs(hosts) do
		self.session.print(host);
	end
	return true, "Done";
end

function def_env.hosts:add(name)
end

def_env.c2s = {};

local function show_c2s(callback)
	for hostname, host in pairs(hosts) do
		for username, user in pairs(host.sessions or {}) do
			for resource, session in pairs(user.sessions or {}) do
				local jid = username.."@"..hostname.."/"..resource;
				callback(jid, session);
			end
		end
	end
end

function def_env.c2s:show(match_jid)
	local print, count = self.session.print, 0;
	local curr_host;
	show_c2s(function (jid, session)
		if curr_host ~= session.host then
			curr_host = session.host;
			print(curr_host);
		end
		if (not match_jid) or jid:match(match_jid) then
			count = count + 1;
			local status, priority = "unavailable", tostring(session.priority or "-");
			if session.presence then
				status = session.presence:child_with_name("show");
				if status then
					status = status:get_text() or "[invalid!]";
				else
					status = "available";
				end
			end
			print("   "..jid.." - "..status.."("..priority..")");
		end		
	end);
	return true, "Total: "..count.." clients";
end

function def_env.c2s:show_insecure(match_jid)
	local print, count = self.session.print, 0;
	show_c2s(function (jid, session)
		if ((not match_jid) or jid:match(match_jid)) and not session.secure then
			count = count + 1;
			print(jid);
		end		
	end);
	return true, "Total: "..count.." insecure client connections";
end

function def_env.c2s:show_secure(match_jid)
	local print, count = self.session.print, 0;
	show_c2s(function (jid, session)
		if ((not match_jid) or jid:match(match_jid)) and session.secure then
			count = count + 1;
			print(jid);
		end		
	end);
	return true, "Total: "..count.." secure client connections";
end

function def_env.c2s:close(match_jid)
	local print, count = self.session.print, 0;
	show_c2s(function (jid, session)
		if jid == match_jid or jid_bare(jid) == match_jid then
			count = count + 1;
			session:close();
		end
	end);
	return true, "Total: "..count.." sessions closed";
end

def_env.s2s = {};
function def_env.s2s:show(match_jid)
	local _print = self.session.print;
	local print = self.session.print;
	
	local count_in, count_out = 0,0;
	
	for host, host_session in pairs(hosts) do
		print = function (...) _print(host); _print(...); print = _print; end
		for remotehost, session in pairs(host_session.s2sout) do
			if (not match_jid) or remotehost:match(match_jid) or host:match(match_jid) then
				count_out = count_out + 1;
				print("    "..host.." -> "..remotehost..(session.secure and " (encrypted)" or "")..(session.compressed and " (compressed)" or ""));
				if session.sendq then
					print("        There are "..#session.sendq.." queued outgoing stanzas for this connection");
				end
				if session.type == "s2sout_unauthed" then
					if session.connecting then
						print("        Connection not yet established");
						if not session.srv_hosts then
							if not session.conn then
								print("        We do not yet have a DNS answer for this host's SRV records");
							else
								print("        This host has no SRV records, using A record instead");
							end
						elseif session.srv_choice then
							print("        We are on SRV record "..session.srv_choice.." of "..#session.srv_hosts);
							local srv_choice = session.srv_hosts[session.srv_choice];
							print("        Using "..(srv_choice.target or ".")..":"..(srv_choice.port or 5269));
						end
					elseif session.notopen then
						print("        The <stream> has not yet been opened");
					elseif not session.dialback_key then
						print("        Dialback has not been initiated yet");
					elseif session.dialback_key then
						print("        Dialback has been requested, but no result received");
					end
				end
			end
		end	
		local subhost_filter = function (h)
				return (match_jid and h:match(match_jid));
			end
		for session in pairs(incoming_s2s) do
			if session.to_host == host and ((not match_jid) or host:match(match_jid)
				or (session.from_host and session.from_host:match(match_jid))
				-- Pft! is what I say to list comprehensions
				or (session.hosts and #array.collect(keys(session.hosts)):filter(subhost_filter)>0)) then
				count_in = count_in + 1;
				print("    "..host.." <- "..(session.from_host or "(unknown)")..(session.secure and " (encrypted)" or "")..(session.compressed and " (compressed)" or ""));
				if session.type == "s2sin_unauthed" then
						print("        Connection not yet authenticated");
				end
				for name in pairs(session.hosts) do
					if name ~= session.from_host then
						print("        also hosts "..tostring(name));
					end
				end
			end
		end
		
		print = _print;
	end
	
	for session in pairs(incoming_s2s) do
		if not session.to_host and ((not match_jid) or session.from_host and session.from_host:match(match_jid)) then
			count_in = count_in + 1;
			print("Other incoming s2s connections");
			print("    (unknown) <- "..(session.from_host or "(unknown)"));			
		end
	end
	
	return true, "Total: "..count_out.." outgoing, "..count_in.." incoming connections";
end

function def_env.s2s:close(from, to)
	local print, count = self.session.print, 0;
	
	if not (from and to) then
		return false, "Syntax: s2s:close('from', 'to') - Closes all s2s sessions from 'from' to 'to'";
	elseif from == to then
		return false, "Both from and to are the same... you can't do that :)";
	end
	
	if hosts[from] and not hosts[to] then
		-- Is an outgoing connection
		local session = hosts[from].s2sout[to];
		if not session then
			print("No outgoing connection from "..from.." to "..to)
		else
			(session.close or s2smanager.destroy_session)(session);
			count = count + 1;
			print("Closed outgoing session from "..from.." to "..to);
		end
	elseif hosts[to] and not hosts[from] then
		-- Is an incoming connection
		for session in pairs(incoming_s2s) do
			if session.to_host == to and session.from_host == from then
				(session.close or s2smanager.destroy_session)(session);
				count = count + 1;
			end
		end
		
		if count == 0 then
			print("No incoming connections from "..from.." to "..to);
		else
			print("Closed "..count.." incoming session"..((count == 1 and "") or "s").." from "..from.." to "..to);
		end
	elseif hosts[to] and hosts[from] then
		return false, "Both of the hostnames you specified are local, there are no s2s sessions to close";
	else
		return false, "Neither of the hostnames you specified are being used on this server";
	end
	
	return true, "Closed "..count.." s2s session"..((count == 1 and "") or "s");
end

def_env.host = {}; def_env.hosts = def_env.host;

function def_env.host:activate(hostname, config)
	return hostmanager.activate(hostname, config);
end
function def_env.host:deactivate(hostname, reason)
	return hostmanager.deactivate(hostname, reason);
end

function def_env.host:list()
	local print = self.session.print;
	local i = 0;
	for host in values(array.collect(keys(prosody.hosts)):sort()) do
		i = i + 1;
		print(host);
	end
	return true, i.." hosts";
end

-------------

function printbanner(session)
	local option = config.get("*", "core", "console_banner");
if option == nil or option == "full" or option == "graphic" then
session.print [[
                   ____                \   /     _       
                    |  _ \ _ __ ___  ___  _-_   __| |_   _ 
                    | |_) | '__/ _ \/ __|/ _ \ / _` | | | |
                    |  __/| | | (_) \__ \ |_| | (_| | |_| |
                    |_|   |_|  \___/|___/\___/ \__,_|\__, |
                    A study in simplicity            |___/ 

]]
end
if option == nil or option == "short" or option == "full" then
session.print("Welcome to the Prosody administration console. For a list of commands, type: help");
session.print("You may find more help on using this console in our online documentation at ");
session.print("http://prosody.im/doc/console\n");
end
if option and option ~= "short" and option ~= "full" and option ~= "graphic" then
	if type(option) == "string" then
		session.print(option)
	elseif type(option) == "function" then
		setfenv(option, redirect_output(_G, session));
		pcall(option, session);
	end
end
end

prosody.net_activate_ports("console", "console", {5582}, "tcp");
