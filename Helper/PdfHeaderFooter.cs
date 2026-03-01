using Analise.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
//using System.Net.Mail;
using System;
using System.Net;



namespace Analise.Helper
{
    public class PdfHeaderFooter : PdfPageEventHelper
    {
        private readonly iTextSharp.text.Font _fonteRodape =
            FontFactory.GetFont(FontFactory.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.GRAY);

        private readonly iTextSharp.text.Font _fonteCabecalho =
            FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        private PdfTemplate _totalPaginas;
        private BaseFont _baseFont;

        private readonly string _logoPath;
        private readonly string _empresa;
        private readonly string _escritura;
        private readonly string _usuario;
        private readonly string _dataHoraGeracao;
        private readonly string _titulo;

        public PdfHeaderFooter(string logoPath, string empresa, string escritura, string titulo, string usuario)
        {
            _logoPath = logoPath;
            _empresa = empresa;
            _escritura = escritura;
            _titulo = titulo;
            _usuario = usuario;
            _dataHoraGeracao = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        }

        // 🔹 Criar template do total de páginas
        public override void OnOpenDocument(PdfWriter writer, Document document)
        {
            _baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            _totalPaginas = writer.DirectContent.CreateTemplate(50, 50);
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            // ================= CABEÇALHO =================
            PdfPTable header = new PdfPTable(1);
            header.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            header.DefaultCell.Border = Rectangle.NO_BORDER;

            if (System.IO.File.Exists(_logoPath))
            {
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(_logoPath);
                logo.ScaleToFit(60f, 60f);
                logo.Alignment = Element.ALIGN_CENTER;

                PdfPCell logoCell = new PdfPCell(logo)
                {
                    Border = Rectangle.NO_BORDER,
                    HorizontalAlignment = Element.ALIGN_CENTER
                };
                header.AddCell(logoCell);
            }

            PdfPCell empresaCell = new PdfPCell(
                new Phrase(_empresa, FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10, BaseColor.BLACK)))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 2f
            };
            header.AddCell(empresaCell);

            PdfPCell escrituraCell = new PdfPCell(
                new Phrase(_escritura, FontFactory.GetFont(FontFactory.HELVETICA_OBLIQUE, 6, BaseColor.DARK_GRAY)))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 2f
            };
            header.AddCell(escrituraCell);

            PdfPCell tituloCell = new PdfPCell(
                new Phrase(_titulo, _fonteCabecalho))
            {
                Border = Rectangle.NO_BORDER,
                HorizontalAlignment = Element.ALIGN_CENTER,
                PaddingBottom = 5f
            };
            header.AddCell(tituloCell);

            header.WriteSelectedRows(0, -1,
                document.LeftMargin,
                document.PageSize.Height - 35,
                writer.DirectContent);

            // ================= RODAPÉ =================
            PdfContentByte cb = writer.DirectContent;

            string textoPagina = "Página " + writer.PageNumber + " de ";
            float tamanhoFonte = 8;

            float larguraTexto = _baseFont.GetWidthPoint(textoPagina, tamanhoFonte);
            float x = (document.Left + document.Right) / 2;
            float y = document.BottomMargin - 10;

            cb.BeginText();
            cb.SetFontAndSize(_baseFont, tamanhoFonte);
            cb.SetTextMatrix(x - (larguraTexto / 2), y);
            cb.ShowText(textoPagina);
            cb.EndText();

            // espaço reservado para total
            cb.AddTemplate(_totalPaginas, x - (larguraTexto / 2) + larguraTexto, y);

            // Texto adicional do rodapé
            ColumnText.ShowTextAligned(
                writer.DirectContent,
                Element.ALIGN_CENTER,
                new Phrase($"Bairro 25 de Junho A, rua 6, ao lado da 16ª Esquadra da PRM, Maputo | Contacto: +258 844 904 555 | Email: thebridemoz@yahoo.com.br | Gerado por: {_usuario} | {_dataHoraGeracao}", _fonteRodape),
                x,
                y - 12,
                0);
        }

        // 🔹 Preencher total no final
        public override void OnCloseDocument(PdfWriter writer, Document document)
        {
            _totalPaginas.BeginText();
            _totalPaginas.SetFontAndSize(_baseFont, 8);
            _totalPaginas.SetTextMatrix(0, 0);
            _totalPaginas.ShowText(writer.PageNumber.ToString()); // <-- aqui
            _totalPaginas.EndText();
        }
    }
}
