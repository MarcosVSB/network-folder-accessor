using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace LogAnalyzer
{
    public partial class NetworkFolderAccessor : Form
    {
        private string[] pastaRede = new string[]
        {
            @"\\brctan484-001\log\ECS\CTA\CGVD",
            @"\\brctan484-001\log\VD\CTVD",
            @"\\brctan484-001\log\ECS\CTA\CTVD",
            @"\\brctan466-001\LOG\Virtual Device"
        };

        private string[] nomesBotoes = new string[]
        {
            "Transmissão (Gearbox) - VD4",
            "Linha Final / Básica VD12",
            "Linha Final / Básica - VD4",
            "Canais VD12"
        };

        private string[] tooltipsText = new string[]
        {
            "Acessar o arquivo VD_CGVD_INFO.log",
            "Acessar o arquivo de log de cada canal individualmente",
            "Acessar o arquivo VD_CTVD_FINLIN_INFO.log para Linha Final, VD_CTVD_D12LIN_INFO.log para DH12 e VD_CTVD_BLKLIN_INFO.log para Linha Básica",
            ""
        };

        private ToolTip tooltip = new ToolTip();

        public NetworkFolderAccessor()
        {
            InitializeComponent();
        }

        private void NetworkFolderAccessor_Load(object sender, EventArgs e)
        {
            this.Text = "Abrir Pastas de Rede";
            this.Size = new System.Drawing.Size(400, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            CreateMenu();
            CreateLabels();
            CreateButtons();
        }

        private void CreateMenu()
        {
            MenuStrip menuStrip = new MenuStrip();
            this.MainMenuStrip = menuStrip;
            this.Controls.Add(menuStrip);

            ToolStripMenuItem menuAjuda = new ToolStripMenuItem("Ajuda");
            menuStrip.Items.Add(menuAjuda);

            ToolStripMenuItem menuAjudaPopup = new ToolStripMenuItem();
            menuAjudaPopup.Text = "Transmissão (Gearbox) - VD4: Acessar o arquivo VD_CGVD_INFO.log\n\n" +
                                  "Linha Final / Básica - VD12: Acessar o arquivo de log de cada canal individualmente\n\n" +
                                  "Linha Final / Básica - VD4: Acessar o arquivo VD_CTVD_FINLIN_INFO.log para Linha Final, VD_CTVD_D12LIN_INFO.log para DH12 e VD_CTVD_BLKLIN_INFO.log para Linha Básica";
            menuAjuda.DropDownItems.Add(menuAjudaPopup);
        }

        private void CreateLabels()
        {
            Label labelPTP = new Label();
            labelPTP.Text = "PTP";
            labelPTP.Location = new System.Drawing.Point(50, 30);
            labelPTP.Size = new System.Drawing.Size(250, 20);
            labelPTP.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
            this.Controls.Add(labelPTP);

            Label labelEBM = new Label();
            labelEBM.Text = "EBM";
            labelEBM.Location = new System.Drawing.Point(50, 230);
            labelEBM.Size = new System.Drawing.Size(250, 20);
            labelEBM.Font = new System.Drawing.Font("Microsoft Sans Serif", 10, System.Drawing.FontStyle.Bold);
            this.Controls.Add(labelEBM);
        }

        private void CreateButtons()
        {
            for (int i = 0; i < pastaRede.Length; i++)
            {
                Button botao = new Button();
                int yPos = i < 3 ? 50 + (i * 50) : 250;
                
                botao.Size = new System.Drawing.Size(250, 40);
                botao.Location = new System.Drawing.Point(50, yPos);
                botao.Text = nomesBotoes[i];
                botao.Tag = pastaRede[i];

                tooltip.SetToolTip(botao, tooltipsText[i]);

                int index = i;
                botao.Click += (sender, e) => AbrirPasta(pastaRede[index]);

                this.Controls.Add(botao);
            }
        }

        private void AbrirPasta(string caminhoRede)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = "explorer.exe",
                    Arguments = caminhoRede,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 350);
            this.Name = "NetworkFolderAccessor";
            this.Load += new System.EventHandler(this.NetworkFolderAccessor_Load);
            this.ResumeLayout(false);
        }
    }
}
