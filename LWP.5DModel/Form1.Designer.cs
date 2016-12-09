namespace LWP._5DModel
{
    partial class Form1
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
            if (disposing && (components != null))
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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.btnMove = new System.Windows.Forms.Button();
            this.btnOrbit = new System.Windows.Forms.Button();
            this.btnWalk = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSetHome = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(12, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1190, 650);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // btnMove
            // 
            this.btnMove.Location = new System.Drawing.Point(378, 668);
            this.btnMove.Name = "btnMove";
            this.btnMove.Size = new System.Drawing.Size(75, 23);
            this.btnMove.TabIndex = 1;
            this.btnMove.Text = "Move";
            this.btnMove.UseVisualStyleBackColor = true;
            this.btnMove.Click += new System.EventHandler(this.btnMove_Click);
            // 
            // btnOrbit
            // 
            this.btnOrbit.Location = new System.Drawing.Point(472, 668);
            this.btnOrbit.Name = "btnOrbit";
            this.btnOrbit.Size = new System.Drawing.Size(75, 23);
            this.btnOrbit.TabIndex = 2;
            this.btnOrbit.Text = "Orbit";
            this.btnOrbit.UseVisualStyleBackColor = true;
            this.btnOrbit.Click += new System.EventHandler(this.btnOrbit_Click);
            // 
            // btnWalk
            // 
            this.btnWalk.Location = new System.Drawing.Point(562, 668);
            this.btnWalk.Name = "btnWalk";
            this.btnWalk.Size = new System.Drawing.Size(75, 23);
            this.btnWalk.TabIndex = 3;
            this.btnWalk.Text = "Walk";
            this.btnWalk.UseVisualStyleBackColor = true;
            this.btnWalk.Click += new System.EventHandler(this.btnWalk_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(653, 668);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(75, 23);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSetHome
            // 
            this.btnSetHome.Location = new System.Drawing.Point(746, 667);
            this.btnSetHome.Name = "btnSetHome";
            this.btnSetHome.Size = new System.Drawing.Size(75, 23);
            this.btnSetHome.TabIndex = 5;
            this.btnSetHome.Text = "SetHome";
            this.btnSetHome.UseVisualStyleBackColor = true;
            this.btnSetHome.Click += new System.EventHandler(this.btnSetHome_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1222, 692);
            this.Controls.Add(this.btnSetHome);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.btnWalk);
            this.Controls.Add(this.btnOrbit);
            this.Controls.Add(this.btnMove);
            this.Controls.Add(this.elementHost1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Button btnMove;
        private System.Windows.Forms.Button btnOrbit;
        private System.Windows.Forms.Button btnWalk;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSetHome;
    }
}

