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
using Footloose.Contracts;
using Footloose.Contracts.Configuration;
using Footloose.DataModel;
using FootlooseExamples.Xmpp.Contracts;

namespace FootlooseExamples.Xmpp.Service
{
    public partial class ServiceForm : Form
    {
        private IFootlooseService footloose;

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
            if (footloose == null)
            {
                var credentials = new NetworkCredential(UserNameTextBox.Text, PasswortTextBox.Text, ServerTextBox.Text);
                var serverAddress = AutoServerResolveCheckBox.Checked ? null : ServerAddressTextBox.Text;
                var endpointId = EndpointIdTextBox.Text;
                var priority = Convert.ToInt32(PriorityNumericUpDown.Value);
                footloose = SetupFootloose(credentials, serverAddress, priority, endpointId);

                EndpointIdentityView.SetEndpointIdentityManager(footloose.EndpointIdentityManager);
            }
            else
            {
                footloose.Connect();
            }

            SetControlStatus(true);
        }

        private void DisconnectButton_Click(object sender, EventArgs e)
        {
            footloose.Disconnect();
            SetControlStatus(false);
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

        private static IFootlooseService SetupFootloose(NetworkCredential credentials, string serverAddress, int priority, string endpointIdentifier)
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
                .CreateFootlooseService();

            return footlooseInstance;
        }

        private static ITransportChannelConfiguration SetupFootlooseTransportChannel(NetworkCredential credentials, string serverAddress, int priority, string endpointIdentifier)
        {
            var transportChannelConfig = Footloose.Configuration.XmppTransportChannelConfiguration.Standard
                .ConnectionType(XmppConnectionType.Tcp)
                .DoNot.UseCompression()
                .UseTls()
                .Credentials(credentials)
                .Priority(priority)
                .EndpointIdentifier(endpointIdentifier);

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
            if(footloose != null && footloose.IsConnected)
            {
                var status = GetStatus();
                var statusInfo = StatusInfoTextBox.Text;
                var priority = Convert.ToInt32(PriorityNumericUpDown.Value);

                footloose.EndpointIdentityManager.SelfEndpointIdentity.UpdatePresence(priority, status, statusInfo);
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
                return CommunicationEndpointStatusType.Unkown;
        }

        private void AutoServerResolveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            ServerAddressTextBox.Enabled = !AutoServerResolveCheckBox.Checked;
        }
    }
}
