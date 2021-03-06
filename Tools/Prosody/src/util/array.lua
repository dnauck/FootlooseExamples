-- Prosody IM
-- Copyright (C) 2008-2010 Matthew Wild
-- Copyright (C) 2008-2010 Waqas Hussain
-- 
-- This project is MIT/X11 licensed. Please see the
-- COPYING file in the source package for more information.
--

local t_insert, t_sort, t_remove, t_concat
    = table.insert, table.sort, table.remove, table.concat;

local array = {};
local array_base = {};
local array_methods = {};
local array_mt = { __index = array_methods, __tostring = function (array) return array:concat(", "); end };

local function new_array(_, t)
	return setmetatable(t or {}, array_mt);
end

function array_mt.__add(a1, a2)
	local res = new_array();
	return res:append(a1):append(a2);
end

setmetatable(array, { __call = new_array });

function array_base.map(outa, ina, func)
	for k,v in ipairs(ina) do
		outa[k] = func(v);
	end
	return outa;
end

function array_base.filter(outa, ina, func)
	local inplace, start_length = ina == outa, #ina;
	local write = 1;
	for read=1,start_length do
		local v = ina[read];
		if func(v) then
			outa[write] = v;
			write = write + 1;
		end
	end
	
	if inplace and write <= start_length then
		for i=write,start_length do
			outa[i] = nil;
		end
	end
	
	return outa;
end

function array_base.sort(outa, ina, ...)
	if ina ~= outa then
		outa:append(ina);
	end
	t_sort(outa, ...);
	return outa;
end

--- These methods only mutate
function array_methods:random()
	return self[math.random(1,#self)];
end

function array_methods:shuffle(outa, ina)
	local len = #self;
	for i=1,#self do
		local r = math.random(i,len);
		self[i], self[r] = self[r], self[i];
	end
	return self;
end

function array_methods:reverse()
	local len = #self-1;
	for i=len,1,-1 do
		self:push(self[i]);
		self:pop(i);
	end
	return self;
end

function array_methods:append(array)
	local len,len2  = #self, #array;
	for i=1,len2 do
		self[len+i] = array[i];
	end
	return self;
end

array_methods.push = table.insert;
array_methods.pop = table.remove;
array_methods.concat = table.concat;
array_methods.length = function (t) return #t; end

--- These methods always create a new array
function array.collect(f, s, var)
	local t, var = {};
	while true do
		var = f(s, var);
	        if var == nil then break; end
		table.insert(t, var);
	end
	return setmetatable(t, array_mt);
end

---

-- Setup methods from array_base
for method, f in pairs(array_base) do
	local base_method = f;
	-- Setup global array method which makes new array
	array[method] = function (old_a, ...)
		local a = new_array();
		return base_method(a, old_a, ...);
	end
	-- Setup per-array (mutating) method
	array_methods[method] = function (self, ...)
		return base_method(self, self, ...);
	end
end

_G.array = array;
module("array");

return array;
