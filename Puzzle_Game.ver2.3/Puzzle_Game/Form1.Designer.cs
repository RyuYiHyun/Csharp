
namespace Puzzle_Game
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.퍼즐사이즈변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x5ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.x5ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.x4ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.이미지변경ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.블레미샤인ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.마젤란ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.수르트ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.자동완성속도조절ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.빠름ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.중간ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.느림ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.퍼즐사이즈변경ToolStripMenuItem,
            this.이미지변경ToolStripMenuItem,
            this.자동완성속도조절ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1084, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 퍼즐사이즈변경ToolStripMenuItem
            // 
            this.퍼즐사이즈변경ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.x4ToolStripMenuItem,
            this.x5ToolStripMenuItem,
            this.x5ToolStripMenuItem1,
            this.x4ToolStripMenuItem1});
            this.퍼즐사이즈변경ToolStripMenuItem.Name = "퍼즐사이즈변경ToolStripMenuItem";
            this.퍼즐사이즈변경ToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.퍼즐사이즈변경ToolStripMenuItem.Text = "퍼즐 사이즈 변경";
            // 
            // x4ToolStripMenuItem
            // 
            this.x4ToolStripMenuItem.Name = "x4ToolStripMenuItem";
            this.x4ToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.x4ToolStripMenuItem.Text = "4X4";
            this.x4ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.x4ToolStripMenuItem_MouseDown);
            // 
            // x5ToolStripMenuItem
            // 
            this.x5ToolStripMenuItem.Name = "x5ToolStripMenuItem";
            this.x5ToolStripMenuItem.Size = new System.Drawing.Size(95, 22);
            this.x5ToolStripMenuItem.Text = "5X5";
            this.x5ToolStripMenuItem.MouseDown += new System.Windows.Forms.MouseEventHandler(this.x5ToolStripMenuItem_MouseDown);
            // 
            // x5ToolStripMenuItem1
            // 
            this.x5ToolStripMenuItem1.Name = "x5ToolStripMenuItem1";
            this.x5ToolStripMenuItem1.Size = new System.Drawing.Size(95, 22);
            this.x5ToolStripMenuItem1.Text = "4X5";
            this.x5ToolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.x5ToolStripMenuItem1_MouseDown);
            // 
            // x4ToolStripMenuItem1
            // 
            this.x4ToolStripMenuItem1.Name = "x4ToolStripMenuItem1";
            this.x4ToolStripMenuItem1.Size = new System.Drawing.Size(95, 22);
            this.x4ToolStripMenuItem1.Text = "5X4";
            this.x4ToolStripMenuItem1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.x4ToolStripMenuItem1_MouseDown);
            // 
            // 이미지변경ToolStripMenuItem
            // 
            this.이미지변경ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.블레미샤인ToolStripMenuItem,
            this.마젤란ToolStripMenuItem,
            this.수르트ToolStripMenuItem});
            this.이미지변경ToolStripMenuItem.Name = "이미지변경ToolStripMenuItem";
            this.이미지변경ToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.이미지변경ToolStripMenuItem.Text = "이미지 변경";
            // 
            // 블레미샤인ToolStripMenuItem
            // 
            this.블레미샤인ToolStripMenuItem.Name = "블레미샤인ToolStripMenuItem";
            this.블레미샤인ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.블레미샤인ToolStripMenuItem.Text = "블레미샤인";
            this.블레미샤인ToolStripMenuItem.Click += new System.EventHandler(this.블레미샤인ToolStripMenuItem_Click);
            // 
            // 마젤란ToolStripMenuItem
            // 
            this.마젤란ToolStripMenuItem.Name = "마젤란ToolStripMenuItem";
            this.마젤란ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.마젤란ToolStripMenuItem.Text = "마젤란";
            this.마젤란ToolStripMenuItem.Click += new System.EventHandler(this.마젤란ToolStripMenuItem_Click);
            // 
            // 수르트ToolStripMenuItem
            // 
            this.수르트ToolStripMenuItem.Name = "수르트ToolStripMenuItem";
            this.수르트ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.수르트ToolStripMenuItem.Text = "수르트";
            this.수르트ToolStripMenuItem.Click += new System.EventHandler(this.수르트ToolStripMenuItem_Click);
            // 
            // 자동완성속도조절ToolStripMenuItem
            // 
            this.자동완성속도조절ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.빠름ToolStripMenuItem,
            this.중간ToolStripMenuItem,
            this.느림ToolStripMenuItem});
            this.자동완성속도조절ToolStripMenuItem.Name = "자동완성속도조절ToolStripMenuItem";
            this.자동완성속도조절ToolStripMenuItem.Size = new System.Drawing.Size(119, 20);
            this.자동완성속도조절ToolStripMenuItem.Text = "자동완성 속도조절";
            // 
            // 빠름ToolStripMenuItem
            // 
            this.빠름ToolStripMenuItem.Name = "빠름ToolStripMenuItem";
            this.빠름ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.빠름ToolStripMenuItem.Text = "빠름";
            this.빠름ToolStripMenuItem.Click += new System.EventHandler(this.빠름ToolStripMenuItem_Click);
            // 
            // 중간ToolStripMenuItem
            // 
            this.중간ToolStripMenuItem.Name = "중간ToolStripMenuItem";
            this.중간ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.중간ToolStripMenuItem.Text = "중간";
            this.중간ToolStripMenuItem.Click += new System.EventHandler(this.중간ToolStripMenuItem_Click);
            // 
            // 느림ToolStripMenuItem
            // 
            this.느림ToolStripMenuItem.Name = "느림ToolStripMenuItem";
            this.느림ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.느림ToolStripMenuItem.Text = "느림";
            this.느림ToolStripMenuItem.Click += new System.EventHandler(this.느림ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.BackColor = System.Drawing.Color.Gray;
            this.ClientSize = new System.Drawing.Size(1084, 861);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(1100, 900);
            this.MinimumSize = new System.Drawing.Size(1100, 900);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "숫자퍼즐게임";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 퍼즐사이즈변경ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem x4ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem x5ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem x5ToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem x4ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 이미지변경ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 블레미샤인ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 마젤란ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 수르트ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 자동완성속도조절ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 빠름ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 중간ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 느림ToolStripMenuItem;
    }
}

