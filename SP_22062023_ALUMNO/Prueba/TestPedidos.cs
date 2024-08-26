using Entidades.Excepciones;
using Entidades.Exceptions;
using Entidades.Modelos;
using Entidades.Files;

namespace Prueba
{
    [TestClass]
    public class TestPedidos
    {
        [TestMethod]
        [ExpectedException(typeof(FileManagerException))]
        public void AlNoRecibirUnaExtensionAdecuada_DeberiaRecibirUnaExcepcion()
        {
            Cocinero<Hamburguesa> cocinero = new Cocinero<Hamburguesa>("Ramon");

            FileManager.Serializar<Cocinero<Hamburguesa>>(cocinero, "hola.jajajaj");
        }

        [TestMethod]
        public void AlInstanciarUnNuevoCocinero_DeberiaTenerLosPedidosEnCero()
        {
            double esperado = 0;
            Cocinero<Hamburguesa> cocinero = new Cocinero<Hamburguesa>("Ramon");

            double resultado = cocinero.TiempoMedioDePreparacion;

            Assert.AreEqual(resultado, esperado);

        }
    }
}