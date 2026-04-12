namespace LibraryManagement.Views;

partial class MainForm
{
	private System.ComponentModel.IContainer components = null;
	private Panel searchPanel = null!;
	private Label searchLabel = null!;
	private TextBox searchTextBox = null!;
	private Button searchButton = null!;
	private Button addButton = null!;
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
		searchPanel = new Panel();
		searchLabel = new Label();
		searchTextBox = new TextBox();
		searchButton = new Button();
		addButton = new Button();
		booksGrid = new DataGridView();
		statusLabel = new Label();
		searchPanel.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)booksGrid).BeginInit();
		SuspendLayout();
		// 
		// searchPanel
		// 
		searchPanel.Controls.Add(addButton);
		searchPanel.Controls.Add(searchButton);
		searchPanel.Controls.Add(searchTextBox);
		searchPanel.Controls.Add(searchLabel);
		searchPanel.Dock = DockStyle.Top;
		searchPanel.Location = new Point(0, 0);
		searchPanel.Name = "searchPanel";
		searchPanel.Padding = new Padding(12, 10, 12, 10);
		searchPanel.Size = new Size(1100, 56);
		searchPanel.TabIndex = 0;
		// 
		// searchLabel
		// 
		searchLabel.AutoSize = true;
		searchLabel.Location = new Point(12, 18);
		searchLabel.Name = "searchLabel";
		searchLabel.Size = new Size(48, 15);
		searchLabel.TabIndex = 0;
		searchLabel.Text = "Search";
		// 
		// searchTextBox
		// 
		searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
		searchTextBox.Location = new Point(72, 14);
		searchTextBox.Name = "searchTextBox";
		searchTextBox.PlaceholderText = "Titre, auteur, genre, ISBN";
		searchTextBox.Size = new Size(790, 23);
		searchTextBox.TabIndex = 1;
		searchTextBox.KeyDown += searchTextBox_KeyDown;
		// 
		// searchButton
		// 
		searchButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		searchButton.Location = new Point(947, 13);
		searchButton.Name = "searchButton";
		searchButton.Size = new Size(70, 25);
		searchButton.TabIndex = 2;
		searchButton.Text = "Search";
		searchButton.UseVisualStyleBackColor = true;
		searchButton.Click += searchButton_Click;
		// 
		// addButton
		// 
		addButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
		addButton.Location = new Point(1023, 13);
		addButton.Name = "addButton";
		addButton.Size = new Size(65, 25);
		addButton.TabIndex = 3;
		addButton.Text = "Add";
		addButton.UseVisualStyleBackColor = true;
		addButton.Click += addButton_Click;
		// 
		// booksGrid
		// 
		booksGrid.AllowUserToAddRows = false;
		booksGrid.AllowUserToDeleteRows = false;
		booksGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
		booksGrid.BackgroundColor = SystemColors.Window;
		booksGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		booksGrid.Dock = DockStyle.Fill;
		booksGrid.Location = new Point(0, 56);
		booksGrid.Margin = new Padding(0);
		booksGrid.MultiSelect = false;
		booksGrid.Name = "booksGrid";
		booksGrid.ReadOnly = true;
		booksGrid.RowHeadersVisible = false;
		booksGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
		booksGrid.Size = new Size(1100, 595);
		booksGrid.TabIndex = 1;
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
		statusLabel.TabIndex = 2;
		statusLabel.Text = "Loading books...";
		// 
		// MainForm
		// 
		AutoScaleDimensions = new SizeF(7F, 15F);
		AutoScaleMode = AutoScaleMode.Font;
		BackColor = SystemColors.Control;
		ClientSize = new Size(1100, 682);
		Controls.Add(searchPanel);
		Controls.Add(booksGrid);
		Controls.Add(statusLabel);
		Font = new Font("Segoe UI", 9F);
		AcceptButton = searchButton;
		MinimumSize = new Size(900, 600);
		Name = "MainForm";
		StartPosition = FormStartPosition.CenterScreen;
		Text = "Library Management";
		searchPanel.ResumeLayout(false);
		searchPanel.PerformLayout();
		((System.ComponentModel.ISupportInitialize)booksGrid).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
