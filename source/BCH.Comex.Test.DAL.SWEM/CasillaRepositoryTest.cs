using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BCH.Comex.Data.DAL.Swift;
using BCH.Comex.Core.Entities.Swift;
using System.Collections.Generic;

namespace BCH.Comex.Test.DAL.SWEM
{
    [TestClass]
    public class CasillaRepositoryTest
    {
        //[TestMethod]
        //public void BuscarSwiftsEnviadosPorCasillaYFechasDevuelveAlgunSwift()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 1);
        //        DateTime fechaHasta = new DateTime(2015, 1, 15);
        //        int total = 0;
        //        int offset = 0;
        //        short fetchRows = 100;
                
        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsEnviadosPorCasillaYFechas(idCasillaExistente, fechaDesde, fechaHasta, out total, offset, fetchRows);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvió ningún swift enviado");
        //    }
        //}

        //[TestMethod]
        //public void BuscarSwiftsRecibidosPorCasillaYFechasDevuelveAlgunSwift()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 1);
        //        DateTime fechaHasta = new DateTime(2015, 1, 15);
        //        int total = 0;
        //        int offset = 0;
        //        short fetchRows = 100;

        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsRecibidosPorCasillaYFechas(idCasillaExistente, fechaDesde, fechaHasta, out total, offset, fetchRows);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvió ningún swift recibido");
        //    }
        //}

        //[TestMethod]
        //public void BuscarSwiftsRecibidosPorCasillaYFechasDevuelveCorrectamenteSiHayUnaSolaPagina()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 2);
        //        DateTime fechaHasta = new DateTime(2015, 1, 2);
        //        int total = 0;
        //        int offset = 0;
        //        short limiteDeRowsQueNoSeVaAExceder = 10000;
                
        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsRecibidosPorCasillaYFechas(idCasillaExistente, fechaDesde, fechaHasta, out total, offset, limiteDeRowsQueNoSeVaAExceder);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvió ningún swift recibido");
        //        Assert.AreEqual(total, resultado.Count, "La cantidad informada no es igual a la devuelta");
        //    }
        //}

        //[TestMethod]
        //public void BuscarSwiftsEnviadosPorCasillaYFechasDevuelveCorrectamenteSiHayUnaSolaPagina()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 2);
        //        DateTime fechaHasta = new DateTime(2015, 1, 2);
        //        int total = 0;
        //        int offset = 0;
        //        short limiteDeRowsQueNoSeVaAExceder = 10000;
                
        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsEnviadosPorCasillaYFechas(idCasillaExistente, fechaDesde, fechaHasta, out total, offset, limiteDeRowsQueNoSeVaAExceder);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvió ningún swift enviado");
        //        Assert.AreEqual(total, resultado.Count, "La cantidad informada no es igual a la devuelta");
        //    }
        //}

        //[TestMethod]
        //public void BuscarSwiftsRecibidosPorCasillaYFechasNoExcedeElTamanioDeLaPagina()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 1);
        //        DateTime fechaHasta = new DateTime(2015, 2, 1);
        //        int total = 0;
        //        int offset = 0;
        //        short limiteDeRowsQueSeVaAExceder = 5;
                
        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsRecibidosPorCasillaYFechas(idCasillaExistente, fechaDesde, fechaHasta, out total, offset, limiteDeRowsQueSeVaAExceder);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvió ningún swift recibido");
        //        Assert.IsTrue(total > resultado.Count, "La cantidad devuelta es mayor a la informada");
        //        Assert.IsTrue(resultado.Count <= limiteDeRowsQueSeVaAExceder, "La cantidad de resultados devuelto es mayor al tamanio de la pagina");
        //    }
        //}

        //[TestMethod]
        //public void BuscarSwiftsEnviadosPorCasillaYFechasNoExcedeElTamanioDeLaPagina()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 1);
        //        DateTime fechaHasta = new DateTime(2015, 2, 1);
        //        int total = 0;
        //        int offset = 0;
        //        short limiteDeRowsQueSeVaAExceder = 5;

        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsEnviadosPorCasillaYFechas(idCasillaExistente, fechaDesde, fechaHasta, out total, offset, limiteDeRowsQueSeVaAExceder);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvió ningún swift recibido");
        //        Assert.IsTrue(total > resultado.Count, "La cantidad devuelta es mayor a la informada");
        //        Assert.IsTrue(resultado.Count <= limiteDeRowsQueSeVaAExceder, "La cantidad de resultados devuelto es mayor al tamanio de la pagina");
        //    }
        //}

        //[TestMethod]
        //public void BuscarSwiftsRecibidosPorCasillaYFechasNoFallaParaCasillaInexistente()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaInexistente = 93446383; //muy dificil que exista esta casilla
        //        DateTime fechaDesde = new DateTime(2015, 1, 2);
        //        DateTime fechaHasta = new DateTime(2015, 1, 2);
        //        int total = 0;
        //        int offset = 0;
        //        short fetchRows = 100;
                
        //        //Act
        //        IList<ResultadoBusquedaSwift> resultado = uow.MensajeRepository.BuscarSwiftsRecibidosPorCasillaYFechas(idCasillaInexistente, fechaDesde, fechaHasta, out total, offset, fetchRows);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado es null");
        //        Assert.IsTrue(resultado.Count == 0, "Se devolvió resultados para una casilla inexistente");
        //    }
        //}

        //[TestMethod]
        //public void GetSwiftEnviadoDevuelveSwiftExistenteCorrectamente()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idNroMensaje = 2530963;
        //        int sesionEsperada = 952;
        //        int secuenciaEsperada = 875897;

        //        //Act
        //        ResultadoBusquedaSwift resultado = uow.MensajeRepository.GetSwiftEnviado(idNroMensaje);

        //        //Assert
        //        Assert.IsNotNull(resultado, "No se pudo obtener el swift existente");
        //        Assert.AreEqual(sesionEsperada, resultado.sesion, "El swift obtenido no tiene el nro sesion esperado");
        //        Assert.AreEqual(secuenciaEsperada, resultado.secuencia, "El swift obtenido no tiene el nro de secuencia esperado");
        //    }
        //}

        //[TestMethod]
        //public void GetSwiftRecibidoDevuelveSwiftExistenteCorrectamente()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int sesionExistente = 952;
        //        int secuenciaExistente = 347792;

        //        //Act
        //        ResultadoBusquedaSwift resultado = uow.MensajeRepository.GetSwiftRecibido(sesionExistente, secuenciaExistente);

        //        //Assert
        //        Assert.IsNotNull(resultado, "No se pudo obtener el swift existente");
        //        Assert.AreEqual(sesionExistente, resultado.sesion, "El swift obtenido no tiene el nro sesion esperado");
        //        Assert.AreEqual(secuenciaExistente, resultado.secuencia, "El swift obtenido no tiene el nro de secuencia esperado");
        //    }
        //}

        //[TestMethod]
        //public void GetEstadisticaMsgPorCasillaDevuelveResultados()
        //{
        //    using (UnitOfWorkSwift uow = new UnitOfWorkSwift())
        //    {
        //        //Arrange
        //        int idCasillaExistente = 753; //unidad servicios M/E
        //        DateTime fechaDesde = new DateTime(2015, 1, 1);
        //        DateTime fechaHasta = new DateTime(2015, 2, 1);

        //        //Act
        //        IList<EstadisticaCasillla> resultado = uow.MensajeRepository.GetEstadisticaMsgPorCasilla(idCasillaExistente, fechaDesde, fechaHasta, MensajeRepository.Direccion.Enviado);

        //        //Assert
        //        Assert.IsNotNull(resultado, "El resultado no puede ser nulo");
        //        Assert.IsTrue(resultado.Count > 0, "No se devolvieron estadísticas para una casilla existente");
        //    }
        //}
    }
}
