using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Footloose.WinForms
{
    public partial class EndpointIdentityView : UserControl
    {
        private IEndpointIdentityManager endpointIdentityManager;

        public EndpointIdentityView()
        {
            InitializeComponent();
        }

        public void SetEndpointIdentityManager(IEndpointIdentityManager endpointIdentityManager)
        {
            this.endpointIdentityManager = endpointIdentityManager;
            BuildEndpointTree();
            SetupEvents();
        }

        public void Clear()
        {
            EndpointTreeView.Nodes.Clear();
        }

        private void SetupEvents()
        {
            endpointIdentityManager.PresenceNotificationReceived += EndpointIdentityManager_PresenceNotificationReceived;
        }

        private void BuildEndpointTree()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(BuildEndpointTree));
                return;
            }

            var endpoints = endpointIdentityManager.EndpointIdentities;

            EndpointTreeView.Nodes.Clear();
            EndpointTreeView.Nodes.AddRange(
                endpoints.Select(
                    identity =>
                    new TreeNode(identity.Key.ToString(),
                                 identity.Value.Endpoints.Select(endpoint => new TreeNode(endpoint.Uri.ToString())).ToArray())).ToArray());
        }

        void EndpointIdentityManager_PresenceNotificationReceived(object sender, PresenceEventArgs e)
        {
            BuildEndpointTree();
        }
    }
}
