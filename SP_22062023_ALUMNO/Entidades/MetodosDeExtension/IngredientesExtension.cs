using Entidades.Enumerados;


namespace Entidades.MetodosDeExtension
{
    public static class IngredientesExtension
    {
        public static double CalcularCostoIngredientes(this List<EIngrediente> ingredientes, int costoInicial)
        {
            double costoTotal = 0;
            costoTotal += costoInicial;
            int porcentajeTotal = 0;
            foreach (EIngrediente ingrediente in ingredientes)
            {
                
                 porcentajeTotal += (int)ingrediente;
                
            }
            costoTotal = costoTotal * ((porcentajeTotal / 100) + 1);
            return costoTotal;
        }

        public static List<EIngrediente> IngredientesAleatorios(this Random rand)
        {
            List<EIngrediente> ingredientes = new List<EIngrediente>()

            {

                EIngrediente.QUESO,

                EIngrediente.PANCETA,

                EIngrediente.ADHERESO,

                EIngrediente.HUEVO,

                EIngrediente.JAMON,

            };

            int random = rand.Next(1, ingredientes.Count + 1);
            return ingredientes.Take(random).ToList();
        }
    }
}
