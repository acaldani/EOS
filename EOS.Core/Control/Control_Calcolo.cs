using EOS.Core.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.VisualBasic;
using DevExpress.XtraEditors;
using DevExpress.XtraRichEdit;
using System.Linq;
using DevExpress.XtraBars;
using System.Dynamic;
using System.Runtime.Remoting;
using System.Reflection;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;


namespace EOS.Core.Control
{

    public struct Struct_Calcolo
    {
        public string CAS;
        public string PesoVolumeConcentrazione;
        public decimal Quantita_Prelevata;
        public decimal Purezza;
        public decimal Acqua;
        public decimal Densita;
        public decimal PMMisurando;
        public decimal PMSostanza;
        public decimal Concentrazione_Iniziale;
        public decimal Peso_Calcolato;
    }

    public class Control_Calcolo
    {
        
        public string ConnectionString;

        public List<String> GetCASbyIDSolution(int IDSoluzione)
        {
            List<String> CASList= new List<String>();
            string SQLString = "";

            SQLString = SQLString + "Select CAS from ";
            SQLString = SQLString + "( ";
            SQLString = SQLString + "select CAS ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details_Concentration] SOLDETCONC ";
            SQLString = SQLString + "where SOLDETCONC.IDSoluzioneMaster in ";
            SQLString = SQLString + "(Select SOLDET.IDSoluzione ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "where isnull(SOLDET.IDSoluzione,0)<>0 ";
            SQLString = SQLString + "and SOLDET.IDSoluzioneMaster={0} ) ";
            SQLString = SQLString + "UNION ";
            SQLString = SQLString + "select SOLDET.CAS ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "inner join [dbo].[Materiale_Tipologia] MATTIP ";
            SQLString = SQLString + "on SOLDET.Tipologia_MR=MATTIP.ID ";
            SQLString = SQLString + "where isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "and MATTIP.MultiploSingolo='Singolo' ";
            SQLString = SQLString + "and SOLDET.IDSoluzioneMaster={0} ";
            SQLString = SQLString + "UNION ";
            SQLString = SQLString + "select WSD.CasComponente as CAS ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "inner join [dbo].[Materiale_Tipologia] MATTIP ";
            SQLString = SQLString + "on SOLDET.Tipologia_MR=MATTIP.ID ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails WSD ";
            SQLString = SQLString + "on WSD.IDSchedaDocumenti=SOLDET.IDSchedaDocumenti ";
            SQLString = SQLString + "where isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "and MATTIP.MultiploSingolo='Multiplo' ";
            SQLString = SQLString + "and SOLDET.IDSoluzioneMaster={0} ";
            SQLString = SQLString + ") CASLIST ";
            SQLString = SQLString + "GROUP BY CAS ";
            SQLString = SQLString + "ORDER BY CAS ";

            SQLString = string.Format(SQLString, IDSoluzione);
              
            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            CASList.Add(Convert.ToString(dr["CAS"]));
                        }
                    }
                    dr = null;
                }
            }
            return CASList;
        }

        public List<Struct_Calcolo> GetStruct_CalcolobyIDsolutionCAS(int IDSoluzione, string CAS)
        {
            List<Struct_Calcolo> Struct_CalcoloList = new List<Struct_Calcolo>();

            string SQLString = "";

            //CONCENTRAZIONE, DA SOUZIONE
            SQLString = SQLString + "select ";
            SQLString = SQLString + "SOLSUBDETCONC.CAS as CAS, ";
            SQLString = SQLString + "'Concentrazione' as PesoVolumeConcentrazione, ";
            SQLString = SQLString + "CASE WHEN isnull(SOLDET.Quantita_Prelievo,0)=0 then 0 else SOLDET.Quantita_Prelievo end as Quantita_Prelevata, ";
            SQLString = SQLString + "0 as Purezza, ";
            SQLString = SQLString + "0 as Acqua, ";
            SQLString = SQLString + "0 as Densita, ";
            SQLString = SQLString + "1 as PMMisurando, ";
            SQLString = SQLString + "1 as PMSostanza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SOLSUBDETCONC.ConcentrazioneFinale,0)=0 then 0 else SOLSUBDETCONC.ConcentrazioneFinale end as Concentrazione_Iniziale, ";
            SQLString = SQLString + "0 as Peso_Calcolato ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "INNER JOIN [dbo].[Soluzioni] SOLSUB ";
            SQLString = SQLString + "ON SOLDET.IDSoluzione=SOLSUB.IDSoluzione ";
            SQLString = SQLString + "INNER JOIN [dbo].[Soluzioni_Details_Concentration] SOLSUBDETCONC ";
            SQLString = SQLString + "ON SOLSUB.IDSoluzione=SOLSUBDETCONC.IDSoluzioneMaster ";
            SQLString = SQLString + "and SOLSUBDETCONC.CAS='{1}' ";
            SQLString = SQLString + "WHERE isnull(SOLDET.IDSoluzione,0)<>0 ";
            SQLString = SQLString + "and isnull(SOLDET.IDSchedaDocumenti,0)=0 ";
            SQLString = SQLString + "and SOLDET.IDSoluzioneMaster={0} ";

            SQLString = string.Format(SQLString, IDSoluzione, CAS);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Struct_Calcolo Struct_CalcoloElement = new Struct_Calcolo();
                            Struct_CalcoloElement.CAS = Convert.ToString(dr["CAS"]);
                            Struct_CalcoloElement.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]);
                            Struct_CalcoloElement.Quantita_Prelevata = Convert.ToDecimal(dr["Quantita_Prelevata"]);
                            Struct_CalcoloElement.Purezza = Convert.ToDecimal(dr["Purezza"]);
                            Struct_CalcoloElement.Acqua = Convert.ToDecimal(dr["Acqua"]);
                            Struct_CalcoloElement.Densita = Convert.ToDecimal(dr["Densita"]);
                            Struct_CalcoloElement.PMMisurando = Convert.ToDecimal(dr["PMMisurando"]);
                            Struct_CalcoloElement.PMSostanza = Convert.ToDecimal(dr["PMSostanza"]);
                            Struct_CalcoloElement.Concentrazione_Iniziale = Convert.ToDecimal(dr["Concentrazione_Iniziale"]);
                            Struct_CalcoloElement.Peso_Calcolato = GetPesoCalcolato(Struct_CalcoloElement);

                            Struct_CalcoloList.Add(Struct_CalcoloElement);
                        }
                    }
                    dr = null;
                }
            }

            SQLString = "";

            //CONCENTRAZIONE, DA WORKING SOLUTION
            SQLString = SQLString + "select ";
            SQLString = SQLString + "WSD.CasComponente as CAS, ";
            SQLString = SQLString + "'Concentrazione' as PesoVolumeConcentrazione, ";
            SQLString = SQLString + "CASE WHEN isnull(SOLDET.Quantita_Prelievo,0)=0 then 0 else SOLDET.Quantita_Prelievo end as Quantita_Prelevata, ";
            SQLString = SQLString + "0 as Purezza, ";
            SQLString = SQLString + "0 as Acqua, ";
            SQLString = SQLString + "0 as Densita, ";
            SQLString = SQLString + "1 as PMMisurando, ";
            SQLString = SQLString + "1 as PMSostanza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(WSD.Concentrazione,0)=0 then 0 else WSD.Concentrazione end as Concentrazione_Iniziale, ";
            SQLString = SQLString + "0 as Peso_Calcolato ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].Dbo.SchedeDocumenti SD ";
            SQLString = SQLString + "on SOLDET.IDSchedaDocumenti=SD.IDSchedaDocumenti ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].DBO.Materiali M ";
            SQLString = SQLString + "ON SD.IDMateriale=M.IDMateriale ";
            SQLString = SQLString + "INNER JOIN Materiale_Tipologia MT ";
            SQLString = SQLString + "ON SOLDET.Tipologia_MR=MT.ID ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].Dbo.WorkingSolutionsDetails WSD ";
            SQLString = SQLString + "ON SD.IDSchedaDocumenti=WSD.IDSchedaDocumenti ";
            SQLString = SQLString + "AND  WSD.CasComponente='{1}' ";
            SQLString = SQLString + "where MT.Nome='Working Solution' ";
            SQLString = SQLString + "and isnull(SOLDET.IDSoluzione,0)=0 ";
            SQLString = SQLString + "and isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "AND SOLDET.IDSoluzioneMaster={0} ";
            SQLString = string.Format(SQLString, IDSoluzione, CAS);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Struct_Calcolo Struct_CalcoloElement = new Struct_Calcolo();
                            Struct_CalcoloElement.CAS = Convert.ToString(dr["CAS"]);
                            Struct_CalcoloElement.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]);
                            Struct_CalcoloElement.Quantita_Prelevata = Convert.ToDecimal(dr["Quantita_Prelevata"]);
                            Struct_CalcoloElement.Purezza = Convert.ToDecimal(dr["Purezza"]);
                            Struct_CalcoloElement.Acqua = Convert.ToDecimal(dr["Acqua"]);
                            Struct_CalcoloElement.Densita = Convert.ToDecimal(dr["Densita"]);
                            Struct_CalcoloElement.PMMisurando = Convert.ToDecimal(dr["PMMisurando"]);
                            Struct_CalcoloElement.PMSostanza = Convert.ToDecimal(dr["PMSostanza"]);
                            Struct_CalcoloElement.Concentrazione_Iniziale = Convert.ToDecimal(dr["Concentrazione_Iniziale"]);
                            Struct_CalcoloElement.Peso_Calcolato = GetPesoCalcolato(Struct_CalcoloElement);

                            Struct_CalcoloList.Add(Struct_CalcoloElement);
                        }
                    }
                    dr = null;
                }
            }


            SQLString = "";

            //CONCENTRAZIONE, DA SOLUZIONE SINGOLA
            SQLString = SQLString + "select ";
            SQLString = SQLString + "M.CAS as CAS, ";
            SQLString = SQLString + "'Concentrazione' as PesoVolumeConcentrazione, ";
            SQLString = SQLString + "CASE WHEN isnull(SOLDET.Quantita_Prelievo,0)=0 then 0 else SOLDET.Quantita_Prelievo end as Quantita_Prelevata, ";
            SQLString = SQLString + "0 as Purezza, ";
            SQLString = SQLString + "0 as Acqua, ";
            SQLString = SQLString + "0 as Densita, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMMisurando end as PMMisurando, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMSostanza end as PMSostanza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Concentrazione,0)=0 then 0 else SD.Concentrazione end as Concentrazione_Iniziale, ";
            SQLString = SQLString + "0 as Peso_Calcolato ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].Dbo.SchedeDocumenti SD ";
            SQLString = SQLString + "on SOLDET.IDSchedaDocumenti=SD.IDSchedaDocumenti ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].DBO.Materiali M ";
            SQLString = SQLString + "ON SD.IDMateriale=M.IDMateriale ";
            SQLString = SQLString + "INNER JOIN Materiale_Tipologia MT ";
            SQLString = SQLString + "ON SOLDET.Tipologia_MR=MT.ID ";
            SQLString = SQLString + "where MT.Nome='Soluzione Singolo Elemento' ";
            SQLString = SQLString + "and isnull(SOLDET.IDSoluzione,0)=0 ";
            SQLString = SQLString + "and isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "AND SOLDET.IDSoluzioneMaster={0} ";
            SQLString = SQLString + "and SOLDET.CAS='{1}' ";

            SQLString = string.Format(SQLString, IDSoluzione, CAS);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Struct_Calcolo Struct_CalcoloElement = new Struct_Calcolo();
                            Struct_CalcoloElement.CAS = Convert.ToString(dr["CAS"]);
                            Struct_CalcoloElement.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]);
                            Struct_CalcoloElement.Quantita_Prelevata = Convert.ToDecimal(dr["Quantita_Prelevata"]);
                            Struct_CalcoloElement.Purezza = Convert.ToDecimal(dr["Purezza"]);
                            Struct_CalcoloElement.Acqua = Convert.ToDecimal(dr["Acqua"]);
                            Struct_CalcoloElement.Densita = Convert.ToDecimal(dr["Densita"]);
                            Struct_CalcoloElement.PMMisurando = Convert.ToDecimal(dr["PMMisurando"]);
                            Struct_CalcoloElement.PMSostanza = Convert.ToDecimal(dr["PMSostanza"]);
                            Struct_CalcoloElement.Concentrazione_Iniziale = Convert.ToDecimal(dr["Concentrazione_Iniziale"]);
                            Struct_CalcoloElement.Peso_Calcolato = GetPesoCalcolato(Struct_CalcoloElement);

                            Struct_CalcoloList.Add(Struct_CalcoloElement);
                        }
                    }
                    dr = null;
                }
            }

            SQLString = "";

            //VOLUME, DA NEAT LIQUIDO
            SQLString = SQLString + "select ";
            SQLString = SQLString + "M.Cas as CAS, ";
            SQLString = SQLString + "'Volume' as PesoVolumeConcentrazione, ";
            SQLString = SQLString + "CASE WHEN isnull(SOLDET.Quantita_Prelievo,0)=0 then 0 else SOLDET.Quantita_Prelievo end as Quantita_Prelevata, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Purezza,0)=0 then 1 else SD.Purezza/100 end as Purezza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Acqua,0)=0 then 0 else SD.Acqua/100 end as Acqua, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Densita,0)=0 then 1 else SD.Densita end as Densita, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMMisurando end as PMMisurando, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMSostanza end as PMSostanza, ";
            SQLString = SQLString + "0 as Concentrazione_Iniziale, ";
            SQLString = SQLString + "0 as Peso_Calcolato ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].Dbo.SchedeDocumenti SD ";
            SQLString = SQLString + "on SOLDET.IDSchedaDocumenti=SD.IDSchedaDocumenti ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].DBO.Materiali M ";
            SQLString = SQLString + "ON SD.IDMateriale=M.IDMateriale ";
            SQLString = SQLString + "INNER JOIN Materiale_Tipologia MT ";
            SQLString = SQLString + "ON SOLDET.Tipologia_MR=MT.ID ";
            SQLString = SQLString + "where MT.Nome='Neat Liquido' ";
            SQLString = SQLString + "and isnull(SOLDET.IDSoluzione,0)=0 ";
            SQLString = SQLString + "and isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "AND SOLDET.IDSoluzioneMaster={0} ";
            SQLString = SQLString + "and SOLDET.CAS='{1}' ";

            SQLString = string.Format(SQLString, IDSoluzione, CAS);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Struct_Calcolo Struct_CalcoloElement = new Struct_Calcolo();
                            Struct_CalcoloElement.CAS = Convert.ToString(dr["CAS"]);
                            Struct_CalcoloElement.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]);
                            Struct_CalcoloElement.Quantita_Prelevata = Convert.ToDecimal(dr["Quantita_Prelevata"]);
                            Struct_CalcoloElement.Purezza = Convert.ToDecimal(dr["Purezza"]);
                            Struct_CalcoloElement.Acqua = Convert.ToDecimal(dr["Acqua"]);
                            Struct_CalcoloElement.Densita = Convert.ToDecimal(dr["Densita"]);
                            Struct_CalcoloElement.PMMisurando = Convert.ToDecimal(dr["PMMisurando"]);
                            Struct_CalcoloElement.PMSostanza = Convert.ToDecimal(dr["PMSostanza"]);
                            Struct_CalcoloElement.Concentrazione_Iniziale = Convert.ToDecimal(dr["Concentrazione_Iniziale"]);
                            Struct_CalcoloElement.Peso_Calcolato = GetPesoCalcolato(Struct_CalcoloElement);

                            Struct_CalcoloList.Add(Struct_CalcoloElement);
                        }
                    }
                    dr = null;
                }
            }

            SQLString = "";

            //PESO, DA NEAT SOLIDO
            SQLString = SQLString + "select ";
            SQLString = SQLString + "M.Cas as CAS, ";
            SQLString = SQLString + "'Peso' as PesoVolumeConcentrazione, ";
            SQLString = SQLString + "CASE WHEN isnull(SOLDET.Quantita_Prelievo,0)=0 then 0 else SOLDET.Quantita_Prelievo end as Quantita_Prelevata, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Purezza,0)=0 then 1 else SD.Purezza/100 end as Purezza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Acqua,0)=0 then 0 else SD.Acqua/100 end as Acqua, ";
            SQLString = SQLString + "0 as Densita, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMMisurando end as PMMisurando, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMSostanza end as PMSostanza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Concentrazione,0)=0 then 0 else SD.Concentrazione end as Concentrazione_Iniziale, ";
            SQLString = SQLString + "0 as Peso_Calcolato ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].Dbo.SchedeDocumenti SD ";
            SQLString = SQLString + "on SOLDET.IDSchedaDocumenti=SD.IDSchedaDocumenti ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].DBO.Materiali M ";
            SQLString = SQLString + "ON SD.IDMateriale=M.IDMateriale ";
            SQLString = SQLString + "INNER JOIN Materiale_Tipologia MT ";
            SQLString = SQLString + "ON SOLDET.Tipologia_MR=MT.ID ";
            SQLString = SQLString + "where MT.Nome='Neat Solido' ";
            SQLString = SQLString + "and isnull(SOLDET.IDSoluzione,0)=0 ";
            SQLString = SQLString + "and isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "AND SOLDET.IDSoluzioneMaster={0} ";
            SQLString = SQLString + "and SOLDET.CAS='{1}' ";

            SQLString = string.Format(SQLString, IDSoluzione, CAS);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Struct_Calcolo Struct_CalcoloElement = new Struct_Calcolo();
                            Struct_CalcoloElement.CAS = Convert.ToString(dr["CAS"]);
                            Struct_CalcoloElement.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]);
                            Struct_CalcoloElement.Quantita_Prelevata = Convert.ToDecimal(dr["Quantita_Prelevata"]);
                            Struct_CalcoloElement.Purezza = Convert.ToDecimal(dr["Purezza"]);
                            Struct_CalcoloElement.Acqua = Convert.ToDecimal(dr["Acqua"]);
                            Struct_CalcoloElement.Densita = Convert.ToDecimal(dr["Densita"]);
                            Struct_CalcoloElement.PMMisurando = Convert.ToDecimal(dr["PMMisurando"]);
                            Struct_CalcoloElement.PMSostanza = Convert.ToDecimal(dr["PMSostanza"]);
                            Struct_CalcoloElement.Concentrazione_Iniziale = Convert.ToDecimal(dr["Concentrazione_Iniziale"]);
                            Struct_CalcoloElement.Peso_Calcolato = GetPesoCalcolato(Struct_CalcoloElement);

                            Struct_CalcoloList.Add(Struct_CalcoloElement);
                        }
                    }
                    dr = null;
                }
            }

            SQLString = "";

            //PESO, DA SOLIDO
            SQLString = SQLString + "select ";
            SQLString = SQLString + "M.Cas as CAS, ";
            SQLString = SQLString + "'Peso' as PesoVolumeConcentrazione, ";
            SQLString = SQLString + "CASE WHEN isnull(SOLDET.Quantita_Prelievo,0)=0 then 0 else SOLDET.Quantita_Prelievo end as Quantita_Prelevata, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Purezza,0)=0 then 1 else SD.Purezza/100 end as Purezza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Acqua,0)=0 then 0 else SD.Acqua/100 end as Acqua, ";
            SQLString = SQLString + "0 as Densita, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMMisurando end as PMMisurando, ";
            SQLString = SQLString + "case when isnull(M.PMMisurando,0)=0 or isnull(M.PMSostanza,0)=0 then 1 else M.PMSostanza end as PMSostanza, ";
            SQLString = SQLString + "CASE WHEN ISNULL(SD.Concentrazione,0)=0 then 0 else SD.Concentrazione end as Concentrazione_Iniziale, ";
            SQLString = SQLString + "0 as Peso_Calcolato ";
            SQLString = SQLString + "from [dbo].[Soluzioni_Details] SOLDET ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].Dbo.SchedeDocumenti SD ";
            SQLString = SQLString + "on SOLDET.IDSchedaDocumenti=SD.IDSchedaDocumenti ";
            SQLString = SQLString + "INNER JOIN [SERVER026].[LUPIN].DBO.Materiali M ";
            SQLString = SQLString + "ON SD.IDMateriale=M.IDMateriale ";
            SQLString = SQLString + "INNER JOIN Materiale_Tipologia MT ";
            SQLString = SQLString + "ON SOLDET.Tipologia_MR=MT.ID ";
            SQLString = SQLString + "where MT.Nome='Solido' ";
            SQLString = SQLString + "and isnull(SOLDET.IDSoluzione,0)=0 ";
            SQLString = SQLString + "and isnull(SOLDET.IDSchedaDocumenti,0)<>0 ";
            SQLString = SQLString + "AND SOLDET.IDSoluzioneMaster={0} ";
            SQLString = SQLString + "and SOLDET.CAS='{1}' ";

            SQLString = string.Format(SQLString, IDSoluzione, CAS);

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    System.Data.SqlClient.SqlDataReader dr;
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Struct_Calcolo Struct_CalcoloElement = new Struct_Calcolo();
                            Struct_CalcoloElement.CAS = Convert.ToString(dr["CAS"]);
                            Struct_CalcoloElement.PesoVolumeConcentrazione = Convert.ToString(dr["PesoVolumeConcentrazione"]);
                            Struct_CalcoloElement.Quantita_Prelevata = Convert.ToDecimal(dr["Quantita_Prelevata"]);
                            Struct_CalcoloElement.Purezza = Convert.ToDecimal(dr["Purezza"]);
                            Struct_CalcoloElement.Acqua = Convert.ToDecimal(dr["Acqua"]);
                            Struct_CalcoloElement.Densita = Convert.ToDecimal(dr["Densita"]);
                            Struct_CalcoloElement.PMMisurando = Convert.ToDecimal(dr["PMMisurando"]);
                            Struct_CalcoloElement.PMSostanza = Convert.ToDecimal(dr["PMSostanza"]);
                            Struct_CalcoloElement.Concentrazione_Iniziale = Convert.ToDecimal(dr["Concentrazione_Iniziale"]);
                            Struct_CalcoloElement.Peso_Calcolato = GetPesoCalcolato(Struct_CalcoloElement);

                            Struct_CalcoloList.Add(Struct_CalcoloElement);
                        }
                    }
                    dr = null;
                }
            }
            return Struct_CalcoloList;
        }

        public decimal GetPesoCalcolato(Struct_Calcolo Struct_CalcoloElement)
        {
            if(Struct_CalcoloElement.PesoVolumeConcentrazione=="Peso")
            {
                Struct_CalcoloElement.Peso_Calcolato = (Struct_CalcoloElement.Quantita_Prelevata * 1000) * (1-Struct_CalcoloElement.Acqua) * Struct_CalcoloElement.Purezza * (Struct_CalcoloElement.PMMisurando/Struct_CalcoloElement.PMSostanza);
            }

            if (Struct_CalcoloElement.PesoVolumeConcentrazione == "Volume")
            {
                Struct_CalcoloElement.Peso_Calcolato = ((Struct_CalcoloElement.Quantita_Prelevata * Struct_CalcoloElement.Densita) * 1000000)*(1- Struct_CalcoloElement.Acqua) * Struct_CalcoloElement.Purezza * (Struct_CalcoloElement.PMMisurando / Struct_CalcoloElement.PMSostanza);
            }

            if (Struct_CalcoloElement.PesoVolumeConcentrazione == "Concentrazione")
            {
                Struct_CalcoloElement.Peso_Calcolato =(Struct_CalcoloElement.Quantita_Prelevata* Struct_CalcoloElement.Concentrazione_Iniziale) * (Struct_CalcoloElement.PMMisurando / Struct_CalcoloElement.PMSostanza);
            }

            return Struct_CalcoloElement.Peso_Calcolato;
        }

        public decimal GetConcentrazioneFinale(List<Struct_Calcolo> Struct_CalcoloList, decimal Volume_Finale)
        {

            decimal Peso_Totale_CAS=0;
            decimal Concentrazione_Finale_CAS = 0;

            foreach (Struct_Calcolo Struct_CalcoloElement in Struct_CalcoloList)
            {
                Peso_Totale_CAS = Peso_Totale_CAS + Struct_CalcoloElement.Peso_Calcolato;
            }

            if(Peso_Totale_CAS!=0)
            {
                Concentrazione_Finale_CAS = Peso_Totale_CAS / Volume_Finale;
            }
            
            return Concentrazione_Finale_CAS;
        }

        public void SetDataScadenza(int IDMaster, int DefaultGiorniScadenza, DateTime DataPreparazione, string SoluzioneSolvente)
        {

            if (CheckAggiornaDataScadenza(IDMaster, SoluzioneSolvente))
            {
                DateTime DataScadenza = Convert.ToDateTime(null);
                DateTime DataScadenzaDefault = Convert.ToDateTime(null);

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                if (ControlCalcolo.CountComponent(IDMaster, SoluzioneSolvente) > 0)
                {
                    string SQLString = "";

                    if (SoluzioneSolvente == "Soluzione")
                    {
                        SQLString = SQLString + "Select min(isnull(DataScadenza,'31/12/2999')) as DataScadenza from Soluzioni_Details where IDSoluzioneMaster={0} and isnull(DataScadenza,'31/12/2999')>getdate() ";
                    }
                    else
                    {
                        SQLString = SQLString + "Select min(isnull(DataScadenza,'31/12/2999')) as DataScadenza from Solventi_Details where IDSolventeMaster={0} and isnull(DataScadenza,'31/12/2999')>getdate() ";
                    }

                    SQLString = string.Format(SQLString, IDMaster);

                    using (var cnn = new SqlConnection())
                    {
                        cnn.ConnectionString = ConnectionString;
                        cnn.Open();

                        using (var cmd = new SqlCommand())
                        {
                            cmd.Connection = cnn;
                            cmd.CommandText = SQLString;

                            System.Data.SqlClient.SqlDataReader dr;
                            dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    DataScadenza = Convert.ToDateTime(dr["DataScadenza"]);
                                }
                            }
                            dr = null;
                        }
                    }
                }

                ControlCalcolo = null;

                if ((DefaultGiorniScadenza != null) && (DataPreparazione.ToString() != "01/01/0001 00:00:00"))
                {
                    if (DefaultGiorniScadenza != 0)
                    {
                        if (DataPreparazione.AddDays(DefaultGiorniScadenza) < DataScadenza)
                        {
                            DataScadenza = DataPreparazione.AddDays(DefaultGiorniScadenza);
                        }
                    }
                }

                

                using (var cnn = new SqlConnection())
                {
                    cnn.ConnectionString = ConnectionString;
                    cnn.Open();

                    SqlCommand cmd;

                    if (SoluzioneSolvente == "Soluzione")
                    {
                        if ((DataScadenza.ToString() == "31/12/2999 00:00:00") || (DataScadenza.ToString() == "01/01/0001 00:00:00"))
                        {
                            cmd = new SqlCommand("Update Soluzioni set DataScadenza = NULL Where IDSoluzione = @ID ");
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Soluzioni set DataScadenza = @DataScadenza Where IDSoluzione = @ID ");
                        }
                    }
                    else
                    {
                        if ((DataScadenza.ToString() == "31/12/2999 00:00:00") || (DataScadenza.ToString() == "01/01/0001 00:00:00"))
                        {
                            cmd = new SqlCommand("Update Solventi set DataScadenza = NULL Where IDSolvente = @ID ");
                        }
                        else
                        {
                            cmd = new SqlCommand("Update Solventi set DataScadenza = @DataScadenza Where IDSolvente = @ID ");
                        }
                    }

                    if((DataScadenza.ToString() == "31/12/2999 00:00:00") || (DataScadenza.ToString() == "01/01/0001 00:00:00"))
                    {
                        
                    }
                    else
                    {
                        cmd.Parameters.Add("@DataScadenza", SqlDbType.DateTime).Value = DataScadenza;
                    }
                        
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = IDMaster;

                    cmd.Connection = cnn;

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CalcolaConcentrazione(int IDSoluzioneToCalculate)
        {
            decimal ConcentrazioneFinale = 0;
            int ret;

            if (IDSoluzioneToCalculate != 0)
            {

                Core.Control.Control_Soluzioni_Details_Concentration ControlSoluzioniDetailsConcentration = new Core.Control.Control_Soluzioni_Details_Concentration();
                ControlSoluzioniDetailsConcentration.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ret = ControlSoluzioniDetailsConcentration.DeleteSolutionDetailsConcentrationByIDSoluzione(IDSoluzioneToCalculate);

                Core.Control.Control_Calcolo ControlCalcolo = new Core.Control.Control_Calcolo();

                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                foreach (string CASCode in ControlCalcolo.GetCASbyIDSolution(IDSoluzioneToCalculate))
                {
                    Core.Model.Model_Soluzioni_Details_Concentration ModelSoluzioniDetailsConcentration = new Core.Model.Model_Soluzioni_Details_Concentration();
                    ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    Core.Control.Controller_Soluzioni ControlSoluzioni = new Core.Control.Controller_Soluzioni();
                    ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                    ConcentrazioneFinale = ControlCalcolo.GetConcentrazioneFinale(ControlCalcolo.GetStruct_CalcolobyIDsolutionCAS(IDSoluzioneToCalculate, CASCode), ControlSoluzioni.GetVolumeFinaleFromIDSoluzione(IDSoluzioneToCalculate));

                    ControlSoluzioni = null;

                    ModelSoluzioniDetailsConcentration.IDSoluzioneMaster = IDSoluzioneToCalculate;
                    ModelSoluzioniDetailsConcentration.CAS = CASCode;
                    ModelSoluzioniDetailsConcentration.ConcentrazioneFinale = ConcentrazioneFinale;
                    ModelSoluzioniDetailsConcentration.DataCalcolo = DateTime.Now;

                    ControlSoluzioniDetailsConcentration.AddSolutionDetailConcentration(ModelSoluzioniDetailsConcentration);
                }

                ControlCalcolo = null;
                ControlSoluzioniDetailsConcentration = null;

                
            }
            else
            {
                XtraMessageBox.Show("E' necessario selezionare una Soluzione MR per eseguire il calcolo delle concentrazioni finali", "Calcola", MessageBoxButtons.OK);
            }
        }

        public int CountComponent(int ID, string SoluzioneSolvente)
        {
            int ret = 0;
            string SQLString = "";

            if (SoluzioneSolvente=="Soluzione")
            {
                SQLString = "Select count(*) as NumeroComponenti from Soluzioni_Details WHERE IDSoluzioneMaster='" + ID + "' ";
            }
            else
            {
                SQLString = "Select count(*) as NumeroComponenti from Solventi_Details WHERE IDSolventeMaster='" + ID + "' ";
            }    

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ret = Convert.ToInt32(dr["NumeroComponenti"]);
                        }
                    }
                }
            }

            return ret;
        }

        public int CountDocument(string CodiceComposto)
        {
            int ret = 0;
            string SQLString = "";

            
            SQLString = "Select count(*) as NumeroDocumenti from Documenti WHERE CodiceComposto='" + CodiceComposto + "' ";

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ret = Convert.ToInt32(dr["NumeroComponenti"]);
                        }
                    }
                }
            }

            return ret;
        }

        public bool CheckAggiornaDataScadenza(int ID, string SoluzioneSolvente)
        {
            bool ret = false;
            string SQLString = "";

            if (SoluzioneSolvente == "Soluzione")
            {
                SQLString = "select Soluzioni.Tipologia from Soluzioni where Soluzioni.IDSoluzione='" + ID + "' ";
            }
            else
            {
                SQLString = "select Solventi.Tipologia from Solventi where Solventi.IDSolvente='" + ID + "' ";
            }

            using (var cnn = new SqlConnection())
            {
                cnn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                cnn.Open();

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = cnn;
                    cmd.CommandText = SQLString;

                    var dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            if (SoluzioneSolvente == "Soluzione")
                            {
                                if(Convert.ToString(dr["Tipologia"])!="Soluzione MR Modello")
                                {
                                    ret = true;
                                }
                                else
                                {
                                    ret = false;
                                }
                            }
                            else
                            {
                                if (Convert.ToString(dr["Tipologia"]) != "Soluzione di Lavoro Modello")
                                {
                                    ret = true;
                                }
                                else
                                {
                                    ret = false;
                                }
                            }
                        }
                    }
                }
            }

            return ret;
        }
    }
}
