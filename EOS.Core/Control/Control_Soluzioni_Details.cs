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

namespace EOS.Core.Control
{
    public class Control_Soluzioni_Details
    {
        public string ConnectionString;
        public int IDUtente=0;

        public IDictionary<int, Model_Soluzioni_Details> GetByIDSolutionDetail(int IDSoluzioneDetail)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni_Details WHERE IDSoluzioneDetail={0}", IDSoluzioneDetail);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Soluzioni_Details> GetByIDSolution(int IDSoluzione)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni_Details WHERE IDSoluzioneMaster={0}", IDSoluzione);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Soluzioni_Details> GetByIDSolutionCAS(int IDSoluzione, string CAS)
        {
            string SQLString = string.Format("SELECT * FROM Soluzioni_Details WHERE IDSoluzioneMaster={0} and CAS='{1}' ", IDSoluzione, CAS);
            return GetData(SQLString);
        }

        public int AddSolutionDetail(Model_Soluzioni_Details SoluzioneDetail)
        {
            int newid = 0;
            string DataScadenza;

            try
            {
                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "INSERT INTO [dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "           ([IDSoluzioneMaster] ";
                SQLString = SQLString + "           ,[IDSchedaDocumenti] ";
                SQLString = SQLString + "           ,[IDSoluzione] ";
                SQLString = SQLString + "           ,[UM_Prelievo] ";
                SQLString = SQLString + "           ,[Quantita_Prelievo] ";
                SQLString = SQLString + "           ,[Concentrazione] ";
                SQLString = SQLString + "           ,[Note] ";
                SQLString = SQLString + "           ,[IDapparecchio] ";
                SQLString = SQLString + "           ,[IDUtensile] ";
                SQLString = SQLString + "           ,[IDapparecchio2] ";
                SQLString = SQLString + "           ,[IDUtensile2] ";
                SQLString = SQLString + "           ,[Tipologia_MR] ";
                SQLString = SQLString + "           ,[CAS] ";
                SQLString = SQLString + "           ,[DataScadenza]) ";
                SQLString = SQLString + "     VALUES ";
                SQLString = SQLString + "           ('{0}' ";
                SQLString = SQLString + "           ,'{1}' ";
                SQLString = SQLString + "           ,'{2}' ";
                SQLString = SQLString + "           ,'{3}' ";
                SQLString = SQLString + "           ,{4} ";
                SQLString = SQLString + "           ,{5} ";
                SQLString = SQLString + "           ,'{6}' ";
                SQLString = SQLString + "           ,{7} ";
                SQLString = SQLString + "           ,'{8}' ";
                SQLString = SQLString + "           ,{9} ";
                SQLString = SQLString + "           ,'{10}' ";
                SQLString = SQLString + "           ,{11} ";
                SQLString = SQLString + "           ,'{12}' ";
                SQLString = SQLString + "           ,Case When '{13}'<>'01/01/0001 00:00:00' then '{13}' else null end); SELECT SCOPE_IDENTITY() ";

                if ((SoluzioneDetail.DataScadenza != null) && (SoluzioneDetail.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = SoluzioneDetail.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, SoluzioneDetail.IDSoluzioneMaster, SoluzioneDetail.IDSchedaDocumenti, SoluzioneDetail.IDSoluzione, SoluzioneDetail.UM_Prelievo.Replace("'", "''"), SoluzioneDetail.Quantita_Prelievo.ToString().Replace(",",".").Replace("'", "''"), SoluzioneDetail.Concentrazione.ToString().Replace(",", ".").Replace("'", "''"), SoluzioneDetail.Note.Replace("'", "''"), SoluzioneDetail.IDApparecchio, SoluzioneDetail.IDUtensile, SoluzioneDetail.IDApparecchio2, SoluzioneDetail.IDUtensile2, SoluzioneDetail.Tipologia_MR, SoluzioneDetail.CAS.Replace("'", "''"), DataScadenza);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                cmd = null;
                cnn = null;

                AddLogSoluzioneDettaglio("Inserimento", SoluzioneDetail, DataScadenza, newid);

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;
                EOS.Core.Control.Controller_Soluzioni ControlSoluzioni = new EOS.Core.Control.Controller_Soluzioni();
                ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new EOS.Core.Model.Model_Soluzioni();
                ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;

                ControlCalcolo.CalcolaConcentrazione(SoluzioneDetail.IDSoluzioneMaster);

                ModelSoluzioni = ControlSoluzioni.GetSolutionByID(SoluzioneDetail.IDSoluzioneMaster).First().Value;
                ControlCalcolo.SetDataScadenza(ModelSoluzioni.IDSoluzione, ModelSoluzioni.DefaultGiorniScadenza, ModelSoluzioni.DataPreparazione, "Soluzione");

                ControlCalcolo = null;
                ControlSoluzioni = null;
                ModelSoluzioni = null;

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateSolutionDetail(Model_Soluzioni_Details SoluzioneDetail)
        {
            string DataScadenza;
            
            try
            {
                Model_Soluzioni_Details SoluzioneDetailOriginale = this.GetByIDSolutionDetail(SoluzioneDetail.IDSoluzioneDetail).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Soluzioni_Details] ";
                SQLString = SQLString + "   SET [IDSoluzioneMaster] = '{0}' ";
                SQLString = SQLString + "      ,[IDSchedaDocumenti] = '{1}' ";
                SQLString = SQLString + "      ,[IDSoluzione] = '{2}' ";
                SQLString = SQLString + "      ,[UM_Prelievo] = '{3}' ";
                SQLString = SQLString + "      ,[Quantita_Prelievo] = {4} ";
                SQLString = SQLString + "      ,[Concentrazione] = {5} ";
                SQLString = SQLString + "      ,[Note] = '{6}' ";
                SQLString = SQLString + "      ,[IDApparecchio] = {7} ";
                SQLString = SQLString + "      ,[IDUtensile] = {8} ";
                SQLString = SQLString + "      ,[IDApparecchio2] = {9} ";
                SQLString = SQLString + "      ,[IDUtensile2] = {10} ";
                SQLString = SQLString + "      ,[Tipologia_MR] = {11} ";
                SQLString = SQLString + "      ,[CAS] = '{12}' ";
                SQLString = SQLString + "      ,[DataScadenza] = Case When '{13}'<>'01/01/0001 00:00:00' then '{13}' else null end ";
                SQLString = SQLString + " WHERE  IDSoluzioneDetail={14} ";

                if ((SoluzioneDetail.DataScadenza != null) && (SoluzioneDetail.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = SoluzioneDetail.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, SoluzioneDetail.IDSoluzioneMaster, SoluzioneDetail.IDSchedaDocumenti, SoluzioneDetail.IDSoluzione, SoluzioneDetail.UM_Prelievo.Replace("'", "''"), SoluzioneDetail.Quantita_Prelievo.ToString().Replace(",", ".").Replace("'", "''"), SoluzioneDetail.Concentrazione.ToString().Replace(",", ".").Replace("'", "''"), SoluzioneDetail.Note.Replace("'", "''"), SoluzioneDetail.IDApparecchio, SoluzioneDetail.IDUtensile, SoluzioneDetail.IDApparecchio2, SoluzioneDetail.IDUtensile2, SoluzioneDetail.Tipologia_MR, SoluzioneDetail.CAS.Replace("'", "''"), DataScadenza, SoluzioneDetail.IDSoluzioneDetail);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                AddLogSoluzioneDettaglio("Aggiornamento", SoluzioneDetail, DataScadenza, 0 , SoluzioneDetailOriginale);

                SoluzioneDetailOriginale = null;

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;
                EOS.Core.Control.Controller_Soluzioni ControlSoluzioni = new EOS.Core.Control.Controller_Soluzioni();
                ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new EOS.Core.Model.Model_Soluzioni();
                ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.CalcolaConcentrazione(SoluzioneDetail.IDSoluzioneMaster);
                ModelSoluzioni = ControlSoluzioni.GetSolutionByID(SoluzioneDetail.IDSoluzioneMaster).First().Value;
                ControlCalcolo.SetDataScadenza(ModelSoluzioni.IDSoluzione, ModelSoluzioni.DefaultGiorniScadenza, ModelSoluzioni.DataPreparazione, "Soluzione");

                ControlSoluzioni.AddLogSoluzione("Aggiornamento", ModelSoluzioni, ModelSoluzioni.DataPreparazione.ToString(), ModelSoluzioni.DataScadenza.ToString(), ModelSoluzioni.DataCreazione.ToString());

                ControlCalcolo = null;
                ControlSoluzioni = null;
                ModelSoluzioni = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogSoluzioneDettaglio(string TipoOperazione, Model_Soluzioni_Details SoluzioneDetail, string DataScadenza, int newid = 0, Model_Soluzioni_Details SoluzioneDetailOriginale=null)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                string SQLStringLog = "";

                if (TipoOperazione == "Cancellazione")
                {
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneMR = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzioneMaster) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneMR (Componente) = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzione) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SoluzioneDetail.IDSchedaDocumenti) + System.Environment.NewLine;
                }
                else 
                {
                    if (TipoOperazione == "Inserimento")
                    {
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneMR = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzioneMaster) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneMR (Componente) = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzione) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SoluzioneDetail.IDSchedaDocumenti) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "TipologiaMR = " + ctlTranscode.GetTipologiaMRByID(SoluzioneDetail.Tipologia_MR) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "CAS = " + SoluzioneDetail.CAS + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "Concentrazione = " + SoluzioneDetail.Concentrazione + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "DataScadenza = " + DataScadenza + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "UMPrelievo = " + SoluzioneDetail.UM_Prelievo + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "QuantitaPrelievo = " + SoluzioneDetail.Quantita_Prelievo + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NumeroApparecchio = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneDetail.IDApparecchio) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NomeUtensile = " + ctlTranscode.GetNomeUtensileByID(SoluzioneDetail.IDUtensile) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NumeroApparecchio2 = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneDetail.IDApparecchio2) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NomeUtensile2 = " + ctlTranscode.GetNomeUtensileByID(SoluzioneDetail.IDUtensile2) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "Note = " + SoluzioneDetail.Note;
                    }
                    else
                    {
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneMR = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzioneMaster) + System.Environment.NewLine;

                        SQLStringLog = SQLStringLog + "CodiceSoluzioneMR (Componente) = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzione) + System.Environment.NewLine;

                        SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SoluzioneDetail.IDSchedaDocumenti) + System.Environment.NewLine;

                        if (SoluzioneDetail.Tipologia_MR != SoluzioneDetailOriginale.Tipologia_MR)
                        {
                            SQLStringLog = SQLStringLog + "TipologiaMR Precedente = " + ctlTranscode.GetTipologiaMRByID(SoluzioneDetailOriginale.Tipologia_MR) + " - Attuale = " + ctlTranscode.GetTipologiaMRByID(SoluzioneDetail.Tipologia_MR) + System.Environment.NewLine;
                        }

                        //if (SoluzioneDetail.IDSoluzione != SoluzioneDetailOriginale.IDSoluzione)
                        //{
                        //    SQLStringLog = SQLStringLog + "CodiceSoluzioneMR Precedente (Componente) = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetailOriginale.IDSoluzione) + " - Attuale = " + ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzione) + System.Environment.NewLine;
                        //}

                        //if (SoluzioneDetail.IDSchedaDocumenti != SoluzioneDetailOriginale.IDSchedaDocumenti)
                        //{
                        //    SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale Precedente (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SoluzioneDetailOriginale.IDSchedaDocumenti) + " - Attuale = " + ctlTranscode.GetIdentificativoELottoByID(SoluzioneDetail.IDSchedaDocumenti) + System.Environment.NewLine;
                        //}

                        if (SoluzioneDetail.CAS != SoluzioneDetailOriginale.CAS)
                        {
                            SQLStringLog = SQLStringLog + "CAS Precedente = " + SoluzioneDetailOriginale.CAS + " - Attuale = " + SoluzioneDetail.CAS + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.Concentrazione != SoluzioneDetailOriginale.Concentrazione)
                        {
                            SQLStringLog = SQLStringLog + "Concentrazione Precedente = " + SoluzioneDetailOriginale.Concentrazione + " - Attuale = " + SoluzioneDetail.Concentrazione + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.DataScadenza != SoluzioneDetailOriginale.DataScadenza)
                        {
                            SQLStringLog = SQLStringLog + "DataScadenza Precedente = " + SoluzioneDetailOriginale.DataScadenza + " - Attuale = " + DataScadenza + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.UM_Prelievo != SoluzioneDetailOriginale.UM_Prelievo)
                        {
                            SQLStringLog = SQLStringLog + "UMPrelievo Precedente = " + SoluzioneDetailOriginale.UM_Prelievo + " - Attuale = " + SoluzioneDetail.UM_Prelievo + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.Quantita_Prelievo != SoluzioneDetailOriginale.Quantita_Prelievo)
                        {
                            SQLStringLog = SQLStringLog + "QuantitaPrelievo Precedente = " + SoluzioneDetailOriginale.Quantita_Prelievo + " - Attuale = " + SoluzioneDetail.Quantita_Prelievo + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.IDApparecchio != SoluzioneDetailOriginale.IDApparecchio)
                        {
                            SQLStringLog = SQLStringLog + "NumeroApparecchio Precedente = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneDetailOriginale.IDApparecchio) + " - Attuale = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneDetail.IDApparecchio) + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.IDUtensile != SoluzioneDetailOriginale.IDUtensile)
                        {
                            SQLStringLog = SQLStringLog + "NomeUtensile Precedente = " + ctlTranscode.GetNomeUtensileByID(SoluzioneDetailOriginale.IDUtensile) + " - Attuale = " + ctlTranscode.GetNomeUtensileByID(SoluzioneDetail.IDUtensile) + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.IDApparecchio2 != SoluzioneDetailOriginale.IDApparecchio2)
                        {
                            SQLStringLog = SQLStringLog + "NumeroApparecchio2 Precedente = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneDetailOriginale.IDApparecchio2) + " - Attuale = " + ctlTranscode.GetCodiceApparecchioByID(SoluzioneDetail.IDApparecchio2) + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.IDUtensile2 != SoluzioneDetailOriginale.IDUtensile2)
                        {
                            SQLStringLog = SQLStringLog + "NomeUtensile2 Precedente = " + ctlTranscode.GetNomeUtensileByID(SoluzioneDetailOriginale.IDUtensile2) + " - Attuale = " + ctlTranscode.GetNomeUtensileByID(SoluzioneDetail.IDUtensile2) + System.Environment.NewLine;
                        }

                        if (SoluzioneDetail.Note != SoluzioneDetailOriginale.Note)
                        {
                            SQLStringLog = SQLStringLog + "Note Precedente = " + SoluzioneDetailOriginale.Note + " - Attuale = " + SoluzioneDetail.Note;
                        }
                    }
                }

                ctlLog.ConnectionString = ConnectionString;

                if (newid != 0)
                {
                    ctlLog.InsertLog(TipoOperazione, "Soluzioni MR Dettaglio", newid, ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzioneMaster), SQLStringLog, IDUtente);
                }
                else
                {
                    ctlLog.InsertLog(TipoOperazione, "Soluzioni MR Dettaglio", SoluzioneDetail.IDSoluzioneDetail, ctlTranscode.GetCodiceSoluzioneByID(SoluzioneDetail.IDSoluzioneMaster), SQLStringLog, IDUtente);
                }

                ctlTranscode = null;
                ctlLog = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int DeleteSolutionDetail(int IDSolutionDetail)
        {
            try
            {
                EOS.Core.Control.Control_Soluzioni_Details ControlSoluzioniDetails = new Core.Control.Control_Soluzioni_Details();
                ControlSoluzioniDetails.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Soluzioni_Details ModelSoluzioniDetails = new EOS.Core.Model.Model_Soluzioni_Details();
                ModelSoluzioniDetails = ControlSoluzioniDetails.GetByIDSolutionDetail(IDSolutionDetail).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Soluzioni_Details] WHERE IDSoluzioneDetail={0} ";
                SQLString = string.Format(SQLString, IDSolutionDetail);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                AddLogSoluzioneDettaglio("Cancellazione", ModelSoluzioniDetails, ModelSoluzioniDetails.DataScadenza.ToString());

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;
                EOS.Core.Control.Controller_Soluzioni ControlSoluzioni = new Core.Control.Controller_Soluzioni();
                ControlSoluzioni.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Soluzioni ModelSoluzioni = new Core.Model.Model_Soluzioni();
                ModelSoluzioni = ControlSoluzioni.GetSolutionByID(ModelSoluzioniDetails.IDSoluzioneMaster).First().Value;

                if (ControlCalcolo.CountComponent(ModelSoluzioniDetails.IDSoluzioneMaster, "Soluzione") > 0)
                {
                    ControlCalcolo.CalcolaConcentrazione(ModelSoluzioniDetails.IDSoluzioneMaster);
                }

                ModelSoluzioni = ControlSoluzioni.GetSolutionByID(ModelSoluzioniDetails.IDSoluzioneMaster).First().Value;
                ControlCalcolo.SetDataScadenza(ModelSoluzioni.IDSoluzione, ModelSoluzioni.DefaultGiorniScadenza, ModelSoluzioni.DataPreparazione, "Soluzione");

                ModelSoluzioni = null;
                ControlSoluzioni = null;
                ControlCalcolo = null;
                ModelSoluzioniDetails = null;
                ControlSoluzioniDetails = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public IDictionary<int, Model_Soluzioni_Details> GetData(string SQLString)
        {
            IDictionary<int, Model_Soluzioni_Details> ret = new Dictionary<int, Model_Soluzioni_Details>();

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
                            Model_Soluzioni_Details r = new Model_Soluzioni_Details();

                            if (DBNull.Value.Equals(dr["IDSoluzioneDetail"])) { r.IDSoluzioneDetail = 0; } else { r.IDSoluzioneDetail = Convert.ToInt32(dr["IDSoluzioneDetail"]); }
                            if (DBNull.Value.Equals(dr["IDSoluzioneMaster"])) { r.IDSoluzioneMaster = 0; } else { r.IDSoluzioneMaster = Convert.ToInt32(dr["IDSoluzioneMaster"]); }
                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["IDSoluzione"])) { r.IDSoluzione = 0; } else { r.IDSoluzione = Convert.ToInt32(dr["IDSoluzione"]); }
                            if (DBNull.Value.Equals(dr["UM_Prelievo"])) { r.UM_Prelievo = ""; } else { r.UM_Prelievo = Convert.ToString(dr["UM_Prelievo"]); }
                            if (DBNull.Value.Equals(dr["Quantita_Prelievo"])) { r.Quantita_Prelievo = 0; } else { r.Quantita_Prelievo = Convert.ToDecimal(dr["Quantita_Prelievo"]); }
                            if (DBNull.Value.Equals(dr["Concentrazione"])) { r.Concentrazione = 0; } else { r.Concentrazione = Convert.ToDecimal(dr["Concentrazione"]); }
                            if (DBNull.Value.Equals(dr["Note"])) { r.Note = ""; } else { r.Note = Convert.ToString(dr["Note"]); }
                            if (DBNull.Value.Equals(dr["IDApparecchio"])) { r.IDApparecchio = 0; } else { r.IDApparecchio = Convert.ToInt32(dr["IDApparecchio"]); }
                            if (DBNull.Value.Equals(dr["IDUtensile"])) { r.IDUtensile = 0; } else { r.IDUtensile = Convert.ToInt32(dr["IDUtensile"]); }
                            if (DBNull.Value.Equals(dr["IDApparecchio2"])) { r.IDApparecchio2 = 0; } else { r.IDApparecchio2 = Convert.ToInt32(dr["IDApparecchio2"]); }
                            if (DBNull.Value.Equals(dr["IDUtensile2"])) { r.IDUtensile2 = 0; } else { r.IDUtensile2 = Convert.ToInt32(dr["IDUtensile2"]); }
                            if (DBNull.Value.Equals(dr["Tipologia_MR"])) { r.Tipologia_MR = 0; } else { r.Tipologia_MR = Convert.ToInt32(dr["Tipologia_MR"]); }
                            if (DBNull.Value.Equals(dr["CAS"])) { r.CAS = ""; } else { r.CAS = Convert.ToString(dr["CAS"]); }
                            if (DBNull.Value.Equals(dr["DataScadenza"])) { r.DataScadenza = Convert.ToDateTime("01/01/0001"); } else { r.DataScadenza = Convert.ToDateTime(dr["DataScadenza"]); }

                            ret.Add(r.IDSoluzioneDetail, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Soluzioni_Details r = new Model_Soluzioni_Details();

                        r.IDSoluzioneDetail = 0;
                        r.IDSoluzioneMaster = 0;
                        r.IDSchedaDocumenti = 0;
                        r.IDSoluzione = 0;
                        r.UM_Prelievo = "";
                        r.Quantita_Prelievo = Convert.ToDecimal(0);
                        r.Concentrazione = Convert.ToDecimal(0);
                        r.Note = "";
                        r.IDApparecchio = 0;
                        r.IDUtensile = 0;
                        r.IDApparecchio2 = 0;
                        r.IDUtensile2 = 0;
                        r.Tipologia_MR = 0;
                        r.CAS = "";
                        r.DataScadenza= Convert.ToDateTime(null);

                        ret.Add(r.IDSoluzioneDetail, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}
