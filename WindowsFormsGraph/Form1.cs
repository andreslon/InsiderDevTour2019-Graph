using MyGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsGraph
{
    public partial class Form1 : Form
    {
        private string ClientId { get; set; } = "fb428ee6-923c-499e-8fb1-920dc480feef";
        // TODO: Connect to Graph
        private Connector connector { get; set; }

        public Form1()
        {
            InitializeComponent();
            connector = new Connector(ClientId);
        }

        private async void Form1_Shown(object sender, EventArgs e)
        {
            // TODO: Initialize UI Component's Data From Graph
            var name = await connector.GetUserNameAsync();

            labelUserName.Text = "User: " + name;

            var events = await connector.GetCalendarEventsAsync();

            listViewCalendar.Items.AddRange(events.Select(ev =>
            {
                var lvi = new ListViewItem(ev.Subject);
                lvi.SubItems.Add(ev.Start.ToDateTimeOffset().LocalDateTime.ToString("g"));
                lvi.SubItems.Add(ev.End.ToDateTimeOffset().LocalDateTime.ToString("g"));
                return lvi;
            }).ToArray());
        }

        private void labelUserName_Click(object sender, EventArgs e)
        {
            // TODO: Collapse Me Before Demo
            connector.LogoutAsync();

            // Restore logged out state.
            labelUserName.Text = "Not Logged In";
            listViewCalendar.Items.Clear();
        }
    }
}
