using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;


public class Parameter
{
    public string Nombre { get; set; }
    public object Valor { get; set; }
    public Parameter(string nom, object val)
    {
        Nombre = nom;
        Valor = val;
    }

    public static SqlCommand LoadToCMD(List<Parameter> parametros, SqlCommand cmd)
    {
        foreach (Parameter p in parametros)
        {
            cmd.Parameters.AddWithValue(p.Nombre, p.Valor);
        }
        return cmd;
    }
}
