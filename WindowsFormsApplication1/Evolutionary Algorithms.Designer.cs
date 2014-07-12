namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_generate = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.gui_cities = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.gui_workers = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.gui_totalLength = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.gui_avgLength = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.localOptBox = new System.Windows.Forms.ComboBox();
            this.localGo = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.gui_evo_growth = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.evoGo = new System.Windows.Forms.Button();
            this.gui_cb_mutation = new System.Windows.Forms.CheckBox();
            this.gui_cb_recomb = new System.Windows.Forms.CheckBox();
            this.gui_evo_popu = new System.Windows.Forms.TextBox();
            this.gui_evo_strategy = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.dateiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.speichernToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.öffnenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eXTRAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.seeDocumentationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpWhereIAmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AccessibleDescription = "Panel for Visualization";
            this.panel1.AccessibleName = "DrawPanel";
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Location = new System.Drawing.Point(338, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(413, 344);
            this.panel1.TabIndex = 0;
            // 
            // button_generate
            // 
            this.button_generate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.button_generate.Location = new System.Drawing.Point(247, 46);
            this.button_generate.Name = "button_generate";
            this.button_generate.Size = new System.Drawing.Size(63, 23);
            this.button_generate.TabIndex = 1;
            this.button_generate.Text = "generate";
            this.button_generate.UseVisualStyleBackColor = true;
            this.button_generate.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Number of Points";
            // 
            // gui_cities
            // 
            this.gui_cities.Location = new System.Drawing.Point(173, 21);
            this.gui_cities.Name = "gui_cities";
            this.gui_cities.Size = new System.Drawing.Size(68, 21);
            this.gui_cities.TabIndex = 3;
            this.gui_cities.Text = "10";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Number of Paths";
            // 
            // gui_workers
            // 
            this.gui_workers.Location = new System.Drawing.Point(173, 47);
            this.gui_workers.Name = "gui_workers";
            this.gui_workers.Size = new System.Drawing.Size(68, 21);
            this.gui_workers.TabIndex = 5;
            this.gui_workers.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label3.Location = new System.Drawing.Point(335, 383);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Total Length";
            // 
            // gui_totalLength
            // 
            this.gui_totalLength.Enabled = false;
            this.gui_totalLength.Location = new System.Drawing.Point(565, 380);
            this.gui_totalLength.Name = "gui_totalLength";
            this.gui_totalLength.Size = new System.Drawing.Size(100, 20);
            this.gui_totalLength.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.label4.Location = new System.Drawing.Point(335, 409);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(167, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Average Length per Path";
            // 
            // gui_avgLength
            // 
            this.gui_avgLength.Enabled = false;
            this.gui_avgLength.Location = new System.Drawing.Point(565, 406);
            this.gui_avgLength.Name = "gui_avgLength";
            this.gui_avgLength.Size = new System.Drawing.Size(100, 20);
            this.gui_avgLength.TabIndex = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.localOptBox);
            this.groupBox1.Controls.Add(this.localGo);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox1.Location = new System.Drawing.Point(12, 113);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 66);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Local Optimization";
            // 
            // localOptBox
            // 
            this.localOptBox.AccessibleDescription = "local Optimization Mode";
            this.localOptBox.AccessibleName = "local Optimization Mode";
            this.localOptBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.localOptBox.FormattingEnabled = true;
            this.localOptBox.Items.AddRange(new object[] {
            "complete local optimization",
            "string inversion only",
            "swap only"});
            this.localOptBox.Location = new System.Drawing.Point(9, 31);
            this.localOptBox.Name = "localOptBox";
            this.localOptBox.Size = new System.Drawing.Size(232, 23);
            this.localOptBox.TabIndex = 11;
            // 
            // localGo
            // 
            this.localGo.AccessibleDescription = "Start local Optimization";
            this.localGo.AccessibleName = "startlocalOpt";
            this.localGo.Enabled = false;
            this.localGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.localGo.Location = new System.Drawing.Point(270, 31);
            this.localGo.Name = "localGo";
            this.localGo.Size = new System.Drawing.Size(40, 23);
            this.localGo.TabIndex = 10;
            this.localGo.Text = "start+";
            this.localGo.UseVisualStyleBackColor = true;
            this.localGo.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.gui_cities);
            this.groupBox2.Controls.Add(this.gui_workers);
            this.groupBox2.Controls.Add(this.button_generate);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 80);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Initialization";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.gui_evo_growth);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.evoGo);
            this.groupBox3.Controls.Add(this.gui_cb_mutation);
            this.groupBox3.Controls.Add(this.gui_cb_recomb);
            this.groupBox3.Controls.Add(this.gui_evo_popu);
            this.groupBox3.Controls.Add(this.gui_evo_strategy);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.groupBox3.Location = new System.Drawing.Point(12, 185);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(320, 134);
            this.groupBox3.TabIndex = 14;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Evolution Strategies";
            // 
            // gui_evo_growth
            // 
            this.gui_evo_growth.Location = new System.Drawing.Point(173, 73);
            this.gui_evo_growth.Name = "gui_evo_growth";
            this.gui_evo_growth.Size = new System.Drawing.Size(68, 21);
            this.gui_evo_growth.TabIndex = 14;
            this.gui_evo_growth.Text = "10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F);
            this.label6.Location = new System.Drawing.Point(6, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "next generation (λ)";
            // 
            // evoGo
            // 
            this.evoGo.AccessibleDescription = "Start local Optimization";
            this.evoGo.AccessibleName = "startlocalOpt";
            this.evoGo.Enabled = false;
            this.evoGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.evoGo.Location = new System.Drawing.Point(270, 100);
            this.evoGo.Name = "evoGo";
            this.evoGo.Size = new System.Drawing.Size(40, 23);
            this.evoGo.TabIndex = 12;
            this.evoGo.Text = "start";
            this.evoGo.UseVisualStyleBackColor = true;
            this.evoGo.Click += new System.EventHandler(this.evoGo_Click);
            // 
            // gui_cb_mutation
            // 
            this.gui_cb_mutation.AutoSize = true;
            this.gui_cb_mutation.Location = new System.Drawing.Point(153, 29);
            this.gui_cb_mutation.Name = "gui_cb_mutation";
            this.gui_cb_mutation.Size = new System.Drawing.Size(77, 20);
            this.gui_cb_mutation.TabIndex = 8;
            this.gui_cb_mutation.Text = "mutation";
            this.gui_cb_mutation.UseVisualStyleBackColor = true;
            this.gui_cb_mutation.CheckedChanged += new System.EventHandler(this.gui_cb_mutation_CheckedChanged);
            // 
            // gui_cb_recomb
            // 
            this.gui_cb_recomb.AutoSize = true;
            this.gui_cb_recomb.Location = new System.Drawing.Point(9, 29);
            this.gui_cb_recomb.Name = "gui_cb_recomb";
            this.gui_cb_recomb.Size = new System.Drawing.Size(112, 20);
            this.gui_cb_recomb.TabIndex = 7;
            this.gui_cb_recomb.Text = "recombination";
            this.gui_cb_recomb.UseVisualStyleBackColor = true;
            this.gui_cb_recomb.CheckedChanged += new System.EventHandler(this.gui_cb_recomb_CheckedChanged);
            // 
            // gui_evo_popu
            // 
            this.gui_evo_popu.Location = new System.Drawing.Point(173, 46);
            this.gui_evo_popu.Name = "gui_evo_popu";
            this.gui_evo_popu.Size = new System.Drawing.Size(68, 21);
            this.gui_evo_popu.TabIndex = 6;
            this.gui_evo_popu.Text = "10";
            // 
            // gui_evo_strategy
            // 
            this.gui_evo_strategy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.gui_evo_strategy.FormattingEnabled = true;
            this.gui_evo_strategy.Items.AddRange(new object[] {
            "(μ + 1)-strategy",
            "(μ + λ)-strategy",
            "(μ, λ)-strategy"});
            this.gui_evo_strategy.Location = new System.Drawing.Point(9, 100);
            this.gui_evo_strategy.Name = "gui_evo_strategy";
            this.gui_evo_strategy.Size = new System.Drawing.Size(232, 23);
            this.gui_evo_strategy.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 52);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(116, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "population size (μ)";
            // 
            // dateiToolStripMenuItem
            // 
            this.dateiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.speichernToolStripMenuItem,
            this.öffnenToolStripMenuItem});
            this.dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            this.dateiToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.dateiToolStripMenuItem.Text = "FILE";
            // 
            // speichernToolStripMenuItem
            // 
            this.speichernToolStripMenuItem.Name = "speichernToolStripMenuItem";
            this.speichernToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.speichernToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.speichernToolStripMenuItem.Text = "Save As";
            this.speichernToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // öffnenToolStripMenuItem
            // 
            this.öffnenToolStripMenuItem.Name = "öffnenToolStripMenuItem";
            this.öffnenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.öffnenToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.öffnenToolStripMenuItem.Text = "Open";
            this.öffnenToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // eXTRAToolStripMenuItem
            // 
            this.eXTRAToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clearAllToolStripMenuItem,
            this.defaultToolStripMenuItem});
            this.eXTRAToolStripMenuItem.Name = "eXTRAToolStripMenuItem";
            this.eXTRAToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.eXTRAToolStripMenuItem.Text = "FUNCTIONS";
            // 
            // clearAllToolStripMenuItem
            // 
            this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.clearAllToolStripMenuItem.Text = "Clear All";
            this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
            // 
            // defaultToolStripMenuItem
            // 
            this.defaultToolStripMenuItem.Name = "defaultToolStripMenuItem";
            this.defaultToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.defaultToolStripMenuItem.Text = "Default";
            this.defaultToolStripMenuItem.Click += new System.EventHandler(this.defaultToolStripMenuItem_Click);
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.seeDocumentationToolStripMenuItem,
            this.helpWhereIAmToolStripMenuItem});
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // seeDocumentationToolStripMenuItem
            // 
            this.seeDocumentationToolStripMenuItem.Name = "seeDocumentationToolStripMenuItem";
            this.seeDocumentationToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.seeDocumentationToolStripMenuItem.Text = "See Documentation";
            this.seeDocumentationToolStripMenuItem.Click += new System.EventHandler(this.seeDocumentationToolStripMenuItem_Click);
            // 
            // helpWhereIAmToolStripMenuItem
            // 
            this.helpWhereIAmToolStripMenuItem.Name = "helpWhereIAmToolStripMenuItem";
            this.helpWhereIAmToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.helpWhereIAmToolStripMenuItem.Text = "context help";
            this.helpWhereIAmToolStripMenuItem.Click += new System.EventHandler(this.helpWhereIAmToolStripMenuItem_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Control;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dateiToolStripMenuItem,
            this.eXTRAToolStripMenuItem,
            this.hELPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(758, 24);
            this.menuStrip1.TabIndex = 11;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 512);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(40, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Status:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(55, 512);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(0, 13);
            this.label8.TabIndex = 17;
            // 
            // chart
            // 
            chartArea1.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart.Legends.Add(legend1);
            this.chart.Location = new System.Drawing.Point(338, 432);
            this.chart.Name = "chart";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart.Series.Add(series1);
            this.chart.Size = new System.Drawing.Size(413, 93);
            this.chart.TabIndex = 18;
            this.chart.Text = "chart1";
            // 
            // Form1
            // 
            this.AccessibleDescription = "Evolutionary Optimization";
            this.AccessibleName = "main_window";
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(758, 534);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.gui_avgLength);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.gui_totalLength);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Evolutionary Optimization";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_generate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox gui_cities;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox gui_workers;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox gui_totalLength;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox gui_avgLength;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox localOptBox;
        private System.Windows.Forms.Button localGo;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox gui_evo_strategy;
        private System.Windows.Forms.TextBox gui_evo_popu;
        private System.Windows.Forms.Button evoGo;
        private System.Windows.Forms.CheckBox gui_cb_mutation;
        private System.Windows.Forms.CheckBox gui_cb_recomb;
        private System.Windows.Forms.TextBox gui_evo_growth;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem dateiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem speichernToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem öffnenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eXTRAToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem defaultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem seeDocumentationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpWhereIAmToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;

    }
}

