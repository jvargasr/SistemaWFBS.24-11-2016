using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFBS.Business.Contracts
{
    /// <summary>
    /// Contrato de Interfaz IOperations
    /// </summary>
    /// <typeparam name="T">Tipo Generico</typeparam>
    public interface IOperations<T>
    {
        /// <summary>
        /// Crear una entidad de una Clase.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        bool Create();
        /// <summary>
        /// Lee una entidad de una Clase.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        bool Read();
        /// <summary>
        /// Actualiza una entidad de una Clase.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        bool Update();
        /// <summary>
        /// Desactiva o Elimina una entidad de una Clase.
        /// </summary>
        /// <returns>Retorna un valor bool acorde a la ejecucion satisfactoria del metodo</returns>
        bool Delete();

        /// <summary>
        /// Lista las entidades de una tabla referente al tipo asignado a la Lista.
        /// </summary>
        /// <returns></returns>
        List<T> Listar();
    }
}
