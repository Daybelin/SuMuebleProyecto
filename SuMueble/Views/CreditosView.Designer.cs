﻿
namespace SuMueble.Views
{
    partial class CreditosView
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btn_pagarCuota = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(469, 187);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(912, 377);
            this.dataGridView1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Light", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(462, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(156, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Buscar Cliente";
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.textBox1.Location = new System.Drawing.Point(469, 133);
            this.textBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.PlaceholderText = "DNI o Nombre de Cliente...";
            this.textBox1.Size = new System.Drawing.Size(234, 34);
            this.textBox1.TabIndex = 2;
            // 
            // btn_pagarCuota
            // 
            this.btn_pagarCuota.BackColor = System.Drawing.Color.DodgerBlue;
            this.btn_pagarCuota.FlatAppearance.BorderSize = 0;
            this.btn_pagarCuota.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_pagarCuota.Font = new System.Drawing.Font("Segoe UI Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_pagarCuota.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btn_pagarCuota.Location = new System.Drawing.Point(469, 585);
            this.btn_pagarCuota.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_pagarCuota.Name = "btn_pagarCuota";
            this.btn_pagarCuota.Size = new System.Drawing.Size(162, 37);
            this.btn_pagarCuota.TabIndex = 3;
            this.btn_pagarCuota.Text = "Pagar Cuota";
            this.btn_pagarCuota.UseVisualStyleBackColor = false;
            this.btn_pagarCuota.Click += new System.EventHandler(this.btn_pagarCuota_Click);
            // 
            // CreditosView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_pagarCuota);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "CreditosView";
            this.Size = new System.Drawing.Size(1810, 760);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btn_pagarCuota;
    }
}
