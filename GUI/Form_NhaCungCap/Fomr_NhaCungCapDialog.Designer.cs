namespace mini_supermarket.GUI.NhaCungCap
{
    partial class Form_NhaCungCapDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

       private void InitializeComponent()
{
    components = new System.ComponentModel.Container();
    this.errorProvider1 = new System.Windows.Forms.ErrorProvider(components);

    // TITLE
    this.Text = "Thông tin nhà cung cấp";
    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
    this.MaximizeBox = false;
    this.MinimizeBox = false;

    // ==== FORM SIZE ====
    this.ClientSize = new System.Drawing.Size(520, 500);

    int xLabel = 20;
    int xInput = 150;
    int widthInput = 330;
    int rowHeight = 70;
    int y = 20;

    // =============== HỌ TÊN ===============
    hoTenLabel = new Label()
    {
        Text = "Tên nhà cung cấp:",
        Location = new Point(xLabel, y + 5),
        AutoSize = true
    };
    hoTenTextBox = new TextBox()
    {
        Location = new Point(xInput, y),
        Width = widthInput
    };
    hoTenErrorIcon = new Label()
    {
        Text = "✕",
        Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput + widthInput + 5, y + 3),
        AutoSize = true,
        Visible = false
    };
    hoTenErrorLabel = new Label()
    {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput, y + 28),
        AutoSize = true
    };
    y += rowHeight;

    // =============== SĐT ===============
    soDienThoaiLabel = new Label()
    {
        Text = "Số điện thoại:",
        Location = new Point(xLabel, y + 5),
        AutoSize = true
    };
    soDienThoaiTextBox = new TextBox()
    {
        Location = new Point(xInput, y),
        Width = widthInput
    };
    soDienThoaiErrorIcon = new Label()
    {
        Text = "✕",
        Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput + widthInput + 5, y + 3),
        AutoSize = true,
        Visible = false
    };
    soDienThoaiErrorLabel = new Label()
    {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput, y + 28),
        AutoSize = true
    };
    y += rowHeight;

    // =============== ĐỊA CHỈ ===============
    diaChiLabel = new Label()
    {
        Text = "Địa chỉ:",
        Location = new Point(xLabel, y + 5),
        AutoSize = true
    };
    diaChiTextBox = new TextBox()
    {
        Location = new Point(xInput, y),
        Width = widthInput
    };
    diaChiErrorIcon = new Label()
    {
        Text = "✕",
        Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput + widthInput + 5, y + 3),
        AutoSize = true,
        Visible = false
    };
    diaChiErrorLabel = new Label()
    {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput, y + 28),
        AutoSize = true
    };
    y += rowHeight;

    // =============== EMAIL ===============
    emailLabel = new Label()
    {
        Text = "Email:",
        Location = new Point(xLabel, y + 5),
        AutoSize = true
    };
    emailTextBox = new TextBox()
    {
        Location = new Point(xInput, y),
        Width = widthInput
    };
    emailErrorIcon = new Label()
    {
        Text = "✕",
        Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput + widthInput + 5, y + 3),
        AutoSize = true,
        Visible = false
    };
    emailErrorLabel = new Label()
    {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput, y + 28),
        AutoSize = true
    };
    y += rowHeight;

    // =============== TRẠNG THÁI ===============
    trangThaiLabel = new Label()
    {
        Text = "Trạng thái:",
        Location = new Point(xLabel, y + 5),
        AutoSize = true
    };
    trangThaiComboBox = new ComboBox()
    {
        Location = new Point(xInput, y),
        Width = widthInput,
        DropDownStyle = ComboBoxStyle.DropDownList
    };
    trangThaiErrorIcon = new Label()
    {
        Text = "✕",
        Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput + widthInput + 5, y + 3),
        AutoSize = true,
        Visible = false
    };
    trangThaiErrorLabel = new Label()
    {
        Font = new System.Drawing.Font("Microsoft Sans Serif", 7.5F),
        ForeColor = System.Drawing.Color.Red,
        Location = new Point(xInput, y + 28),
        AutoSize = true
    };
    y += 50;

    // =============== MÃ NCC ===============
    maNhaCungCapLabel = new Label()
    {
        Text = "Mã NCC:",
        Location = new Point(xLabel, y + 5),
        AutoSize = true
    };
    maNhaCungCapValueLabel = new Label()
    {
        Location = new Point(xInput, y + 6),
        AutoSize = true
    };
    y += 70;

    // =============== BUTTONS ===============
    okButton = new Button()
    {
        Text = "OK",
        Width = 100,
        Height = 32,
        Location = new Point(270, y)
    };
    cancelButton = new Button()
    {
        Text = "Hủy",
        Width = 100,
        Height = 32,
        Location = new Point(380, y)
    };

    // ErrorProvider
    errorProvider1.ContainerControl = this;

    // ==== ADD CONTROLS ====
    this.Controls.AddRange(new Control[]
    {
        hoTenLabel, hoTenTextBox, hoTenErrorIcon, hoTenErrorLabel,
        soDienThoaiLabel, soDienThoaiTextBox, soDienThoaiErrorIcon, soDienThoaiErrorLabel,
        diaChiLabel, diaChiTextBox, diaChiErrorIcon, diaChiErrorLabel,
        emailLabel, emailTextBox, emailErrorIcon, emailErrorLabel,
        trangThaiLabel, trangThaiComboBox, trangThaiErrorIcon, trangThaiErrorLabel,
        maNhaCungCapLabel, maNhaCungCapValueLabel,
        okButton, cancelButton
    });

    this.ResumeLayout(false);
}


        #endregion

        private System.Windows.Forms.Label hoTenLabel;
        private System.Windows.Forms.TextBox hoTenTextBox;
        private System.Windows.Forms.Label hoTenErrorIcon;
        private System.Windows.Forms.Label hoTenErrorLabel;

        private System.Windows.Forms.Label soDienThoaiLabel;
        private System.Windows.Forms.TextBox soDienThoaiTextBox;
        private System.Windows.Forms.Label soDienThoaiErrorIcon;
        private System.Windows.Forms.Label soDienThoaiErrorLabel;

        private System.Windows.Forms.Label diaChiLabel;
        private System.Windows.Forms.TextBox diaChiTextBox;
        private System.Windows.Forms.Label diaChiErrorIcon;
        private System.Windows.Forms.Label diaChiErrorLabel;

        private System.Windows.Forms.Label emailLabel;
        private System.Windows.Forms.TextBox emailTextBox;
        private System.Windows.Forms.Label emailErrorIcon;
        private System.Windows.Forms.Label emailErrorLabel;

        private System.Windows.Forms.Label trangThaiLabel;
        private System.Windows.Forms.ComboBox trangThaiComboBox;
        private System.Windows.Forms.Label trangThaiErrorIcon;
        private System.Windows.Forms.Label trangThaiErrorLabel;

        private System.Windows.Forms.Label maNhaCungCapLabel;
        private System.Windows.Forms.Label maNhaCungCapValueLabel;

        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;

        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
