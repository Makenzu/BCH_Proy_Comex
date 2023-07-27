using BCH.Comex.Core.BL.Portal;
using BCH.Comex.Core.BL.Portal.Fakes;
using BCH.Comex.Core.BL.Portal.Users;
using BCH.Comex.Core.Entities.Portal;
using BCH.Comex.Data.DAL.Fakes;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BCH.Comex.Test.BL.Portal
{
    [TestClass()]
    public class PortalServiceTests
    {
        
        [TestMethod()]
        public void ListGruposTest()
        {
            using (ShimsContext.Create())
            {

                var listaGrupos = new List<GrupoAplicacion>() { new GrupoAplicacion() { id = "1", name = "one" } };

                var grupoAppsRepo = new Data.DAL.Portal.Fakes.ShimGruposAplicacionesRepository();
                var baseRepo = new ShimGenericRepository<GrupoAplicacion, BCH.Comex.Data.DAL.Portal.portalEntities>(grupoAppsRepo) {
                    GetAll = () => listaGrupos
                };

                var unitOfWork = new Data.DAL.Portal.Fakes.ShimUnitOfWorkPortal()
                {
                    GruposAplicacionesRepositoryGet = () => grupoAppsRepo
                };

                var service = new PortalService(unitOfWork);
                var grupos = service.ListGrupos();

                Assert.AreEqual(listaGrupos.Count, grupos.Count);
            }
        }

        [TestMethod()]
        public void GetUserAppsTest()
        {
            using (ShimsContext.Create())
            {
                var listaGrupos = new List<GrupoAplicacion>() { new GrupoAplicacion() { id = "1", name = "one" } };

                var listaApps = new List<Aplicacion>() {
                    new Aplicacion() { id = "1", name = "one", ad_group_name ="a", Grupo=listaGrupos.First() },
                    new Aplicacion() { id = "2", name = "two", ad_group_name ="c", Grupo=listaGrupos.First() }
                };


                var appsRepo = new Data.DAL.Portal.Fakes.ShimAplicacionesRepository();
                var baseRepo = new ShimGenericRepository<Aplicacion, BCH.Comex.Data.DAL.Portal.portalEntities>(appsRepo)
                {
                    GetAll = () => listaApps
                };

                var unitOfWork = new Data.DAL.Portal.Fakes.ShimUnitOfWorkPortal()
                {
                    AplicacionesRepositoryGet = () => appsRepo
                };


                var membershipProvider = new ShimUserMembershipProvider()
                {
                    GetGroupsComexUserBoolean = (user, refresh) => new List<string> { "a", "b" }
                };

                var service = new PortalService(unitOfWork, membershipProvider);
                var apps = service.GetUserApps(ComexUser.Create(Thread.CurrentPrincipal));

                Assert.AreEqual(1, apps.Keys.Count);
                Assert.AreEqual(1, apps[apps.Keys.First()].Count);
            }
        }

        [TestMethod()]
        public void GetDatosUsuarioTest()
        {
            using (ShimsContext.Create())
            {

                //var datosUsuarioExpected = new DatosUsuarioDTO() { };

                //var datosUsuarioRepository = new Data.DAL.Portal.Fakes.ShimDatosUsuarioRepository();
                //var baseRepo = new ShimGenericRepository<IDatosUsuario, BCH.Comex.Data.DAL.Portal.portalEntities>(datosUsuarioRepository)
                //{
                //    GetByIDObject = (id) => datosUsuarioExpected
                //};

                //var unitOfWork = new Data.DAL.Portal.Fakes.ShimUnitOfWorkPortal()
                //{
                //    DatosUsuarioRepositoryGet = () => datosUsuarioRepository
                //};

                //var service = new PortalService(unitOfWork);

                ////ComexUser.PortalService = service;

                //var datosUsuario = service.GetDatosUsuario(ComexUser.Create(Thread.CurrentPrincipal));

                //Assert.AreEqual(datosUsuarioExpected, datosUsuario);
            }
        }

        [TestMethod()]
        public void RegisterAppTest()
        {
            using (ShimsContext.Create())
            {
                Aplicacion savedApp = null;
                GrupoAplicacion savedGrupoApp = null;

                var aplicationParaRegistrar = new Aplicacion()
                {
                    id = "124",
                    controller = "controller",
                    ad_group_name = "adGroupName",
                    Grupo = new GrupoAplicacion()
                    {
                        id = "4545",
                        name = "grupo"
                    }
                };

                var aplicacionesRepository = new Data.DAL.Portal.Fakes.ShimAplicacionesRepository();
                var baseRepo = new ShimGenericRepository<Aplicacion, BCH.Comex.Data.DAL.Portal.portalEntities>(aplicacionesRepository)
                {
                    InsertT0 = (app) => { savedApp = app; },
                    GetByIDObject = (id) => null
                };

                var grupoAplicacionesRepository = new Data.DAL.Portal.Fakes.ShimGruposAplicacionesRepository();
                var baseRepoGrupo = new ShimGenericRepository<GrupoAplicacion, BCH.Comex.Data.DAL.Portal.portalEntities>(grupoAplicacionesRepository)
                {
                    GetByIDObject = (id) => null,
                    InsertT0 = (grupoApp) => { savedGrupoApp = grupoApp; }
                };

                var unitOfWork = new Data.DAL.Portal.Fakes.ShimUnitOfWorkPortal()
                {
                    AplicacionesRepositoryGet = () => aplicacionesRepository,
                    GruposAplicacionesRepositoryGet = () => grupoAplicacionesRepository,
                    Save = () => { }
                };

                var service = new PortalService(unitOfWork);

                service.RegisterApp(aplicationParaRegistrar.id, aplicationParaRegistrar.name, aplicationParaRegistrar.Grupo.name, aplicationParaRegistrar.ad_group_name, aplicationParaRegistrar.Grupo.id, aplicationParaRegistrar.controller);

                Assert.AreEqual(aplicationParaRegistrar.Grupo.id, savedGrupoApp.id);
                Assert.AreEqual(aplicationParaRegistrar.Grupo.name, savedGrupoApp.name);
                Assert.AreEqual(aplicationParaRegistrar.id, savedApp.id);
                Assert.AreEqual(aplicationParaRegistrar.name, savedApp.name);
                Assert.AreEqual(aplicationParaRegistrar.ad_group_name, savedApp.ad_group_name);
                Assert.AreEqual(aplicationParaRegistrar.controller, savedApp.controller);
                Assert.AreEqual(aplicationParaRegistrar.action, savedApp.action);
                Assert.AreEqual(aplicationParaRegistrar.parameters, savedApp.parameters);
            }
        }
    }
}