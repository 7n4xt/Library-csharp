namespace LibraryManagement.Views;

partial class MainForm
{
	private System.ComponentModel.IContainer components = null;
	private DataGridView booksGrid = null!;
	private Label statusLabel = null!;

	protected override void Dispose(bool disposing)
	{
		if (disposing && (components != null))
		{
			components.Dispose();
		}

		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		booksGrid = new DataGridView();
		statusLabel = new Label();
		((System.ComponentModel.ISupportInitialize)booksGrid).BeginInit();
		SuspendLayout();
		// 
		// booksGrid
		// 
		booksGrid.AllowUserToAddRows = false;
		booksGrid.AllowUserToDeleteRows = false;
		booksGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		booksGrid.BackgroundColor = SystemColors.Window;
		booksGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		booksGrid.Dock = DockStyle.Fill;
		booksGrid.Location = new Point(0, 0);
		booksGrid.Margin = new Padding(0);
		booksGrid.MultiSelect = false;
		booksGrid.Name = "booksGrid";
		booksGrid.ReadOnly = true;
		booksGrid.RowHeadersVisible = false;
		booksGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		booksGrid.Size = new Size(1100, 651);
		booksGrid.TabIndex = 0;
		// 
		// statusLabel
		// 
		statusLabel.AutoSize = true;
		statusLabel.BackColor = SystemColors.Control;
		statusLabel.Dock = DockStyle.Bottom;
		statusLabel.ForeColor = SystemColors.ControlText;
		statusLabel.Location = new Point(0, 651);
		statusLabel.Margin = new Padding(8);
		statusLabel.Name = "statusLabel";
		statusLabel.Padding = new Padding(12, 8, 12, 8);
		statusLabel.Size = new Size(132, 31);
		statusLabel.TabIndex = 1;
		statusLabel.Text = "Loading books...";
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.Control;
		ClientSize = new Size(1100, 682);
		Controls.Add(booksGrid);
		Controls.Add(statusLabel);
		Font = new Font("Segoe UI", 9F);
		MinimumSize = new Size(900, 600);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Library Management";
		((System.ComponentModel.ISupportInitialize)booksGrid).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
