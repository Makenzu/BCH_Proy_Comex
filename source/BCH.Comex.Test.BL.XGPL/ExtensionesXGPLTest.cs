using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCH.Comex.Core.BL.XGPL;

namespace BCH.Comex.Test.BL.XGPL
{
    [TestClass]
    public class ExtensionesXGPLTest
    {
        [TestMethod]
        public void TestFormatoDecimal()
        {
            Assert.AreEqual("1.000.000,00", 1000000m.FormatoDecimal());
            Assert.AreEqual("1.000,00", 1000m.FormatoDecimal());
            Assert.AreEqual("0,00", 0m.FormatoDecimal());
            Assert.AreEqual("-10.000,00", (-10000m).FormatoDecimal());
        }

        [TestMethod]
        public void TestFormatoNumeroPlanilla()
        {
            Assert.AreEqual("", FrmgPlv.FormatoOrden(""));
        }

        [TestMethod]
        public void TestNombreModelo()
        {
            DateTime d;
            DateTime.TryParse("2014-04-30", out d);
            Assert.AreEqual("Planillas Visibles de Exportación ingresadas el 30-04-2014 - Especialista: Maria I. Sepulveda",
                FrmgPlv.NombreModelo(FrmgPlv.TipoListado.tplVisibleExportacion, "Maria I. Sepulveda", d));
            Assert.AreEqual("Planillas Visibles de Exportación - Especialista: Maria I. Sepulveda",
                            FrmgPlv.NombreModelo(FrmgPlv.TipoListado.tplVisibleExportacion, "Maria I. Sepulveda", null));
            Assert.AreEqual("Planillas Visibles de Exportación (sin información de usuario)",
                                        FrmgPlv.NombreModelo(FrmgPlv.TipoListado.tplVisibleExportacion, null, null));
            Assert.AreEqual("Planillas Visibles de Exportación ingresadas el 30-04-2014 (sin información de usuario)",
                                        FrmgPlv.NombreModelo(FrmgPlv.TipoListado.tplVisibleExportacion, null, d));
            Assert.AreEqual("Planillas Visibles de Importaciones Endosadas - Especialista: Maria I. Sepulveda",
                FrmgPlv.NombreModelo(FrmgPlv.TipoListado.tplVisibleImportacionEndosadas, "Maria I. Sepulveda", d));
        }

        [TestMethod]
        public void TestCommaSeparatedArray()
        {
            Assert.AreEqual(0, new CommaSeparatedCompactableArray("").Length, "Array de largo 0");
            Assert.AreEqual(0, new CommaSeparatedCompactableArray(null).Length, "Array desde string nulo");
            Assert.AreEqual(0, new CommaSeparatedCompactableArray("     ").Length, "Array desde string en blanco");
            Assert.AreEqual(0, new CommaSeparatedCompactableArray(";").Length, "Array de elementos vacíos");
            Assert.AreEqual(0, new CommaSeparatedCompactableArray("; ;").Length, "Array de elementos vacíos");

            Assert.AreEqual("", (new CommaSeparatedCompactableArray(""))[10], "Index fuera de rango retorna string en blanco");
            Assert.AreEqual("", (new CommaSeparatedCompactableArray(""))[-1], "Index fuera de rango retorna string en blanco");
            var array = new CommaSeparatedCompactableArray("0;1;2;3");
            Assert.AreEqual("0", array[0], "Obtener elemento");
            Assert.AreEqual("1", array[1], "Obtener elemento");
            Assert.AreEqual("2", array[2], "Obtener elemento");
            Assert.AreEqual("3", array[3], "Obtener elemento");
            Assert.AreEqual("", array[4], "Obtener elemento");

            array[100] = "100";

            Assert.AreEqual(5, array.Length);
            Assert.AreEqual("0;1;2;3;100", array.Value);

            array = new CommaSeparatedCompactableArray("0");
            Assert.AreEqual("0", array[0], "Obtener elemento");
            Assert.AreEqual("", array[1], "Obtener elemento");
            array = new CommaSeparatedCompactableArray(";;;0;;;");
            Assert.AreEqual("0", array[0], "Obtener elemento");
            Assert.AreEqual("", array[1], "Obtener elemento");
        }
    }
}
