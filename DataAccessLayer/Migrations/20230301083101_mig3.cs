using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FIRMALAR",
                columns: table => new
                {
                    firmaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firmaadi = table.Column<string>(type: "Varchar(100)", nullable: true),
                    vergidairesi = table.Column<string>(type: "Varchar(60)", nullable: true),
                    vergino = table.Column<string>(type: "Varchar(40)", nullable: true),
                    adresbilgisi = table.Column<string>(type: "Varchar(100)", nullable: true),
                    telefon = table.Column<string>(type: "Varchar(16)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FIRMALAR", x => x.firmaID);
                });

            migrationBuilder.CreateTable(
                name: "KULLANICILAR",
                columns: table => new
                {
                    kullaniciID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    kullaniciadi = table.Column<string>(type: "Varchar(50)", nullable: true),
                    sifre = table.Column<string>(nullable: true),
                    stokgirisi = table.Column<bool>(nullable: false),
                    stokgirisisilme = table.Column<bool>(nullable: false),
                    stokgirisduzenleme = table.Column<bool>(nullable: false),
                    stokdurumu = table.Column<bool>(nullable: false),
                    stokcikisi = table.Column<bool>(nullable: false),
                    stokcikisduzenleme = table.Column<bool>(nullable: false),
                    siparisiptali = table.Column<bool>(nullable: false),
                    siparisgirisi = table.Column<bool>(nullable: false),
                    firmagirisi = table.Column<bool>(nullable: false),
                    satinalma = table.Column<bool>(nullable: false),
                    projeler = table.Column<bool>(nullable: false),
                    raporlar = table.Column<bool>(nullable: false),
                    rezerveacma = table.Column<bool>(nullable: false),
                    kullanicipaneli = table.Column<bool>(nullable: false),
                    siparisduzenleme = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KULLANICILAR", x => x.kullaniciID);
                });

            migrationBuilder.CreateTable(
                name: "PROJELER",
                columns: table => new
                {
                    projeID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    projekodu = table.Column<int>(nullable: false),
                    projeadi = table.Column<string>(nullable: true),
                    proje = table.Column<string>(type: "Varchar(100)", nullable: true),
                    firma = table.Column<string>(type: "Varchar(100)", nullable: true),
                    anlasmatutari = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PROJELER", x => x.projeID);
                });

            migrationBuilder.CreateTable(
                name: "STOKKARTI",
                columns: table => new
                {
                    stokkodu = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urunadi = table.Column<string>(type: "Varchar(50)", nullable: true),
                    uretici = table.Column<string>(type: "Varchar(50)", nullable: true),
                    yuzey = table.Column<string>(type: "Varchar(50)", nullable: true),
                    boy = table.Column<decimal>(nullable: false),
                    en = table.Column<decimal>(nullable: false),
                    kalinlik = table.Column<decimal>(nullable: false),
                    birim = table.Column<string>(nullable: true),
                    tanim = table.Column<string>(type: "Varchar(100)", nullable: true),
                    tarih = table.Column<DateTime>(nullable: false),
                    minmiktar = table.Column<decimal>(nullable: false),
                    maxmiktar = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOKKARTI", x => x.stokkodu);
                });

            migrationBuilder.CreateTable(
                name: "SIPARIS",
                columns: table => new
                {
                    siparisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stokkodu = table.Column<int>(nullable: false),
                    STOKKARTIstokkodu = table.Column<int>(nullable: true),
                    projekodu = table.Column<int>(nullable: false),
                    PROJELERprojeID = table.Column<int>(nullable: true),
                    firmaID = table.Column<int>(nullable: false),
                    FIRMALARfirmaID = table.Column<int>(nullable: true),
                    siparisFirmaID = table.Column<int>(nullable: false),
                    kullaniciID = table.Column<int>(nullable: false),
                    KULLANICILARkullaniciID = table.Column<int>(nullable: true),
                    satinalmaID = table.Column<int>(nullable: false),
                    taleptarihi = table.Column<DateTime>(nullable: true),
                    termintarihi = table.Column<DateTime>(nullable: true),
                    tarih = table.Column<DateTime>(nullable: true),
                    aciklama = table.Column<string>(type: "Varchar(100)", nullable: true),
                    saciklama = table.Column<string>(type: "Varchar(100)", nullable: true),
                    siparisiptal = table.Column<bool>(nullable: false),
                    miktar = table.Column<decimal>(nullable: false),
                    alinanmiktar = table.Column<decimal>(nullable: false),
                    kalanmiktar = table.Column<decimal>(nullable: false),
                    stokeklenenmiktar = table.Column<decimal>(nullable: false),
                    durumu = table.Column<string>(type: "Varchar(20)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIPARIS", x => x.siparisID);
                    table.ForeignKey(
                        name: "FK_SIPARIS_FIRMALAR_FIRMALARfirmaID",
                        column: x => x.FIRMALARfirmaID,
                        principalTable: "FIRMALAR",
                        principalColumn: "firmaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIPARIS_KULLANICILAR_KULLANICILARkullaniciID",
                        column: x => x.KULLANICILARkullaniciID,
                        principalTable: "KULLANICILAR",
                        principalColumn: "kullaniciID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIPARIS_PROJELER_PROJELERprojeID",
                        column: x => x.PROJELERprojeID,
                        principalTable: "PROJELER",
                        principalColumn: "projeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SIPARIS_STOKKARTI_STOKKARTIstokkodu",
                        column: x => x.STOKKARTIstokkodu,
                        principalTable: "STOKKARTI",
                        principalColumn: "stokkodu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STOKDURUMU",
                columns: table => new
                {
                    stokdurumuID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stokkodu = table.Column<int>(nullable: false),
                    STOKKARTIstokkodu = table.Column<int>(nullable: true),
                    ortalamabirimfiyati = table.Column<decimal>(nullable: false),
                    miktar = table.Column<decimal>(nullable: false),
                    toplamtutar = table.Column<decimal>(nullable: false),
                    rezervealinmismiktar = table.Column<decimal>(nullable: false),
                    kullanilabilirmiktar = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOKDURUMU", x => x.stokdurumuID);
                    table.ForeignKey(
                        name: "FK_STOKDURUMU_STOKKARTI_STOKKARTIstokkodu",
                        column: x => x.STOKKARTIstokkodu,
                        principalTable: "STOKKARTI",
                        principalColumn: "stokkodu",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "STOKGIRISI",
                columns: table => new
                {
                    stokgirisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stokkodu = table.Column<int>(nullable: false),
                    STOKKARTIstokkodu = table.Column<int>(nullable: true),
                    projekodu = table.Column<int>(nullable: false),
                    PROJELERprojeID = table.Column<int>(nullable: true),
                    firmaID = table.Column<int>(nullable: false),
                    FIRMALARfirmaID = table.Column<int>(nullable: true),
                    kullaniciID = table.Column<int>(nullable: false),
                    KULLANICILARkullaniciID = table.Column<int>(nullable: true),
                    siparisID = table.Column<int>(nullable: false),
                    faturano = table.Column<string>(type: "Varchar(20)", nullable: true),
                    faturatarihi = table.Column<DateTime>(nullable: false),
                    irsaliyeno = table.Column<string>(type: "Varchar(20)", nullable: true),
                    irsaliyetarihi = table.Column<DateTime>(nullable: false),
                    tarih = table.Column<DateTime>(nullable: false),
                    kdv = table.Column<decimal>(nullable: false),
                    birimfiyati = table.Column<decimal>(nullable: false),
                    kdvdahilbirimfiyati = table.Column<decimal>(nullable: false),
                    miktar = table.Column<decimal>(nullable: false),
                    toplamtutar = table.Column<decimal>(nullable: false),
                    kdvtutari = table.Column<decimal>(nullable: false),
                    kullanilabilirmiktar = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOKGIRISI", x => x.stokgirisID);
                    table.ForeignKey(
                        name: "FK_STOKGIRISI_FIRMALAR_FIRMALARfirmaID",
                        column: x => x.FIRMALARfirmaID,
                        principalTable: "FIRMALAR",
                        principalColumn: "firmaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKGIRISI_KULLANICILAR_KULLANICILARkullaniciID",
                        column: x => x.KULLANICILARkullaniciID,
                        principalTable: "KULLANICILAR",
                        principalColumn: "kullaniciID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKGIRISI_PROJELER_PROJELERprojeID",
                        column: x => x.PROJELERprojeID,
                        principalTable: "PROJELER",
                        principalColumn: "projeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKGIRISI_STOKKARTI_STOKKARTIstokkodu",
                        column: x => x.STOKKARTIstokkodu,
                        principalTable: "STOKKARTI",
                        principalColumn: "stokkodu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKGIRISI_SIPARIS_siparisID",
                        column: x => x.siparisID,
                        principalTable: "SIPARIS",
                        principalColumn: "siparisID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "STOKCIKISI",
                columns: table => new
                {
                    stokcikisID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stokkodu = table.Column<int>(nullable: false),
                    STOKKARTIstokkodu = table.Column<int>(nullable: true),
                    projekodu = table.Column<int>(nullable: false),
                    PROJELERprojeID = table.Column<int>(nullable: true),
                    kullaniciID = table.Column<int>(nullable: false),
                    KULLANICILARkullaniciID = table.Column<int>(nullable: true),
                    stokgirisID = table.Column<int>(nullable: false),
                    stokgirisID1 = table.Column<int>(nullable: true),
                    birimfiyati = table.Column<decimal>(nullable: false),
                    miktar = table.Column<decimal>(nullable: false),
                    toplamtutar = table.Column<decimal>(nullable: false),
                    tarih = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_STOKCIKISI", x => x.stokcikisID);
                    table.ForeignKey(
                        name: "FK_STOKCIKISI_KULLANICILAR_KULLANICILARkullaniciID",
                        column: x => x.KULLANICILARkullaniciID,
                        principalTable: "KULLANICILAR",
                        principalColumn: "kullaniciID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKCIKISI_PROJELER_PROJELERprojeID",
                        column: x => x.PROJELERprojeID,
                        principalTable: "PROJELER",
                        principalColumn: "projeID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKCIKISI_STOKKARTI_STOKKARTIstokkodu",
                        column: x => x.STOKKARTIstokkodu,
                        principalTable: "STOKKARTI",
                        principalColumn: "stokkodu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_STOKCIKISI_STOKGIRISI_stokgirisID1",
                        column: x => x.stokgirisID1,
                        principalTable: "STOKGIRISI",
                        principalColumn: "stokgirisID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_FIRMALARfirmaID",
                table: "SIPARIS",
                column: "FIRMALARfirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_KULLANICILARkullaniciID",
                table: "SIPARIS",
                column: "KULLANICILARkullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_PROJELERprojeID",
                table: "SIPARIS",
                column: "PROJELERprojeID");

            migrationBuilder.CreateIndex(
                name: "IX_SIPARIS_STOKKARTIstokkodu",
                table: "SIPARIS",
                column: "STOKKARTIstokkodu");

            migrationBuilder.CreateIndex(
                name: "IX_STOKCIKISI_KULLANICILARkullaniciID",
                table: "STOKCIKISI",
                column: "KULLANICILARkullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_STOKCIKISI_PROJELERprojeID",
                table: "STOKCIKISI",
                column: "PROJELERprojeID");

            migrationBuilder.CreateIndex(
                name: "IX_STOKCIKISI_STOKKARTIstokkodu",
                table: "STOKCIKISI",
                column: "STOKKARTIstokkodu");

            migrationBuilder.CreateIndex(
                name: "IX_STOKCIKISI_stokgirisID1",
                table: "STOKCIKISI",
                column: "stokgirisID1");

            migrationBuilder.CreateIndex(
                name: "IX_STOKDURUMU_STOKKARTIstokkodu",
                table: "STOKDURUMU",
                column: "STOKKARTIstokkodu");

            migrationBuilder.CreateIndex(
                name: "IX_STOKGIRISI_FIRMALARfirmaID",
                table: "STOKGIRISI",
                column: "FIRMALARfirmaID");

            migrationBuilder.CreateIndex(
                name: "IX_STOKGIRISI_KULLANICILARkullaniciID",
                table: "STOKGIRISI",
                column: "KULLANICILARkullaniciID");

            migrationBuilder.CreateIndex(
                name: "IX_STOKGIRISI_PROJELERprojeID",
                table: "STOKGIRISI",
                column: "PROJELERprojeID");

            migrationBuilder.CreateIndex(
                name: "IX_STOKGIRISI_STOKKARTIstokkodu",
                table: "STOKGIRISI",
                column: "STOKKARTIstokkodu");

            migrationBuilder.CreateIndex(
                name: "IX_STOKGIRISI_siparisID",
                table: "STOKGIRISI",
                column: "siparisID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "STOKCIKISI");

            migrationBuilder.DropTable(
                name: "STOKDURUMU");

            migrationBuilder.DropTable(
                name: "STOKGIRISI");

            migrationBuilder.DropTable(
                name: "SIPARIS");

            migrationBuilder.DropTable(
                name: "FIRMALAR");

            migrationBuilder.DropTable(
                name: "KULLANICILAR");

            migrationBuilder.DropTable(
                name: "PROJELER");

            migrationBuilder.DropTable(
                name: "STOKKARTI");
        }
    }
}
