using System;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

public class KosztorysForm : Form
{
    private TextBox txtProjekt, txtUsluga, txtCena;
    private Button btnGeneruj;

    public KosztorysForm()
    {
        this.Text = "Generator Kosztorysu v1.0";
        this.Size = new Size(350, 300);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;

        // Etykiety i pola tekstowe
        Label lbl1 = new Label() { Text = "Nazwa projektu:", Left = 20, Top = 20, Width = 200 };
        txtProjekt = new TextBox() { Left = 20, Top = 40, Width = 280 };

        Label lbl2 = new Label() { Text = "Opis usługi:", Left = 20, Top = 70, Width = 200 };
        txtUsluga = new TextBox() { Left = 20, Top = 90, Width = 280 };

        Label lbl3 = new Label() { Text = "Cena (PLN):", Left = 20, Top = 120, Width = 200 };
        txtCena = new TextBox() { Left = 20, Top = 140, Width = 280 };

        // Przycisk
        btnGeneruj = new Button()
        {
            Text = "ZAPISZ KOSZTORYS NA PULPICIE (PDF)",
            Left = 20,
            Top = 190,
            Width = 280,
            Height = 40,
            BackColor = Color.LightGreen
        };
        btnGeneruj.Click += BtnGeneruj_Click;

        this.Controls.Add(lbl1);
        this.Controls.Add(txtProjekt);
        this.Controls.Add(lbl2);
        this.Controls.Add(txtUsluga);
        this.Controls.Add(lbl3);
        this.Controls.Add(txtCena);
        this.Controls.Add(btnGeneruj);
    }

    private void BtnGeneruj_Click(object sender, EventArgs e)
    {
        try
        {
            string pulpit = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string nazwaPliku = "Kosztorys_" + txtProjekt.Text + ".pdf";
            string sciezka = Path.Combine(pulpit, nazwaPliku);

            Document dokument = new Document();
            PdfWriter.GetInstance(dokument, new FileStream(sciezka, FileMode.Create));
            dokument.Open();

            dokument.Add(new Paragraph($"KOSZTORYS"));
            dokument.Add(new Paragraph($"Data: {DateTime.Now}"));
            dokument.Add(new Paragraph($"Projekt: {txtProjekt.Text}"));
            dokument.Add(new Paragraph($"Usługa: {txtUsluga.Text}"));
            dokument.Add(new Paragraph($"Suma: {txtCena.Text} PLN"));
            dokument.Add(new Paragraph("--------------------------"));
            dokument.Add(new Paragraph("Wygenerowano automatycznie."));

            dokument.Close();

            MessageBox.Show($"Sukces! Kosztorys w formacie PDF znajdziesz na pulpicie jako:\n\n{nazwaPliku}", "Zapisano");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Błąd: " + ex.Message);
        }
    }

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new KosztorysForm());
    }
}
