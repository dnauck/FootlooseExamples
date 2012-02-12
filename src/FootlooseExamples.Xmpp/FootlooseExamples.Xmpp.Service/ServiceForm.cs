using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Footloose;
using Footloose.Configuration;
using Footloose.Configuration.Fluent;
using Footloose.DataModel;
using FootlooseExamples.Xmpp.Contracts;
using ConnectionState = Footloose.ConnectionState;

namespace FootlooseExamples.Xmpp.Service
{
    public partial class ServiceForm : Form
    {
        private IFootlooseConnection footlooseConnection;

        public ServiceForm()
        {
            InitializeComponent();
            SetControlStatus(false);
        }

        private void ServiceForm_Load(object sender, EventArgs e)
        {

        }

        private void ConnectButton_Click(object sender, EventArgs e)
        {
            if (footlooseConnection == null)
            {
                var credentials = new NetworkCredential(UserNameTextBox.Text, PasswortTextBox.Text, ServerTextBox.Text);
                var serverAddress = AutoServerResolveCheckBox.Checked ? null : ServerAddressTextBox.Text;
                var endpointId = EndpointIdTextBox.Text;
                var priority = Convert.ToInt32(PriorityNumericUpDown.Value);
                footlooseConnection = SetupFootlooseConnection(credentials, serverAddress, priority, endpointId);
                footlooseConnection.ConnectionStateChanged += FootlooseConnection_ConnectionStateChanged;

                EndpointIdentityView.SetEndpointIdentityManager(footlooseConnection.EndpointIdentityManager);
            }

            footlooseConnection.Open();
        }

        void FootlooseConnection_ConnectionStateChanged(object sender, ConnectionStateEventArgs e)
        {
            SetControlStatus(e.ConnectionState == ConnectionState.Connected);
            Text = "Footloose Service - Connection Status is " + e.ConnectionState;
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            footlooseConnection.Close();
        }

        private void SetControlStatus(bool connected)
        {
            ConnectButton.Enabled = !connected;
            DisconnectButton.Enabled = connected;
            UserNameTextBox.Enabled = !connected;
            PasswortTextBox.Enabled = !connected;
            ServerTextBox.Enabled = !connected;
            AutoServerResolveCheckBox.Enabled = !connected;
            ServerAddressTextBox.Enabled = (!AutoServerResolveCheckBox.Checked && !connected);
            EndpointIdTextBox.Enabled = !connected;
            PriorityNumericUpDown.Enabled = connected;
            StatusInfoTextBox.Enabled = connected;
            SendPresenceButton.Enabled = connected;
            OnlineStatusRadioButton.Enabled = connected;
            BusyStatusRadioButton.Enabled = connected;
            TemporarilyUnavailableStatusRadioButton.Enabled = connected;
            UnavailableStatusRadioButton.Enabled = connected;
            EndpointIdentityView.Enabled = connected;
            if(!connected)
                EndpointIdentityView.Clear();
        }

        private static IFootlooseConnection SetupFootlooseConnection(NetworkCredential credentials, string serverAddress, int priority, string endpointIdentifier)
        {
            var footlooseInstance = Fluently.Configure()
                .SerializerOfType<Footloose.Serialization.BinarySerializer>()
                .ServiceLocator(new ServiceLocatorDummy())
                .ServiceContracts(contracts =>
                                      {
                                          //automaticly register all public interfaces that are in the "*.Contracts" namespace
                                          contracts.AutoServiceContract.RegisterFromAssemblyOf
                                              <IDataSetNorthwindRepository>().
                                              Where(
                                                  type =>
                                                  type.IsInterface &&
                                                  type.IsPublic &&
                                                  type.Namespace.EndsWith("Contracts"));
                                      }
                )
                .TransportChannel(() => SetupFootlooseTransportChannel(credentials, serverAddress, priority, endpointIdentifier))
                .CreateFootlooseConnection();

            return footlooseInstance;
        }

        private static IFluentTransportChannelConfiguration SetupFootlooseTransportChannel(NetworkCredential credentials, string serverAddress, int priority, string endpointIdentifier)
        {
            var transportChannelConfig = Footloose.Configuration.Fluent.XmppTransportChannelConfiguration.Standard
                .ConnectionType(XmppConnectionType.Tcp)
                .DoNot.UseCompression()
                .UseTls()
                .Credentials(credentials)
                .Priority(priority)
                .EndpointIdentifier(endpointIdentifier)
                .MaxMessageBodyLength(20000);

            if (string.IsNullOrEmpty(serverAddress))
            {
                transportChannelConfig.AutoResolveServerAddress();
            }
            else
            {
                transportChannelConfig
                    .DoNot.AutoResolveServerAddress()
                    .ServerAddress(serverAddress);
            }

            return transportChannelConfig;
        }

        private void SendPresenceButton_Click(object sender, EventArgs e)
        {
            if(footlooseConnection != null && footlooseConnection.ConnectionState == ConnectionState.Connected)
            {
                var status = GetStatus();
                var statusInfo = StatusInfoTextBox.Text;
                var priority = Convert.ToInt32(PriorityNumericUpDown.Value);

                footlooseConnection.EndpointIdentityManager.SelfEndpointIdentity.UpdatePresence(priority, status, statusInfo);
            }
        }

        private CommunicationEndpointStatusType GetStatus()
        {
            if (OnlineStatusRadioButton.Checked)
                return CommunicationEndpointStatusType.Online;

            else if (BusyStatusRadioButton.Checked)
                return CommunicationEndpointStatusType.Busy;

            else if (TemporarilyUnavailableStatusRadioButton.Checked)
                return CommunicationEndpointStatusType.TemporarilyUnavailable;

            else if (UnavailableStatusRadioButton.Checked)
                return CommunicationEndpointStatusType.Unavailable;

            else
                return CommunicationEndpointStatusType.Online;
        }

        private void AutoServerResolveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ServerAddressTextBox.Enabled = !AutoServerResolveCheckBox.Checked;
        }
    }
}
