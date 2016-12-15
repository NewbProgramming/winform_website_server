namespace winform_website_server
{
    partial class website_server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
        if(disposing && (components != null))
        {
        components.Dispose();
        }
        base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(website_server));
            this.icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.icon_menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.icon_menu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.icon_menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // icon
            // 
            this.icon.ContextMenuStrip = this.icon_menu;
            this.icon.Icon = ((System.Drawing.Icon)(resources.GetObject("icon.Icon")));
            this.icon.Text = "C# Website Server";
            this.icon.Visible = true;
            this.icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.icon_MouseDoubleClick);
            // 
            // icon_menu
            // 
            this.icon_menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.icon_menu_exit});
            this.icon_menu.Name = "icon_menu";
            this.icon_menu.Size = new System.Drawing.Size(93, 26);
            // 
            // icon_menu_exit
            // 
            this.icon_menu_exit.Name = "icon_menu_exit";
            this.icon_menu_exit.Size = new System.Drawing.Size(92, 22);
            this.icon_menu_exit.Text = "Exit";
            this.icon_menu_exit.Click += new System.EventHandler(this.icon_menu_exit_Click);
            // 
            // website_server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(606, 188);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "website_server";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Website Server";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.website_server_Load);
            this.icon_menu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon icon;
        private System.Windows.Forms.ContextMenuStrip icon_menu;
        private System.Windows.Forms.ToolStripMenuItem icon_menu_exit;
    }
}

