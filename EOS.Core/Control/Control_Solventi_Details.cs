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
    public class Control_Solventi_Details
    {
        public string ConnectionString;
        public int IDUtente=0;

        public IDictionary<int, Model_Solventi_Details> GetByIDSolventeDetail(int IDSolventeDetail)
        {
            string SQLString = string.Format("SELECT * FROM Solventi_Details WHERE IDSolventeDetail={0}", IDSolventeDetail);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Solventi_Details> GetByIDSolvente(int IDSolvente)
        {
            string SQLString = string.Format("SELECT * FROM Solventi_Details WHERE IDSolventeMaster={0}", IDSolvente);
            return GetData(SQLString);
        }

        public IDictionary<int, Model_Solventi_Details> GetByIDSolventeCAS(int IDSolvente, string CAS)
        {
            string SQLString = string.Format("SELECT * FROM Solventi_Details WHERE IDSolventeMaster={0} and CAS='{1}' ", IDSolvente, CAS);
            return GetData(SQLString);
        }

        public int AddSolventeDetail(Model_Solventi_Details SolventeDetail)
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
                SQLString = SQLString + "INSERT INTO [dbo].[Solventi_Details] ";
                SQLString = SQLString + "           ([IDSolventeMaster] ";
                SQLString = SQLString + "           ,[IDSchedaDocumenti] ";
                SQLString = SQLString + "           ,[IDSolvente] ";
                SQLString = SQLString + "           ,[UM_Prelievo] ";
                SQLString = SQLString + "           ,[Quantita_Prelievo] ";
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
                SQLString = SQLString + "           ,'{5}' ";
                SQLString = SQLString + "           ,{6} ";
                SQLString = SQLString + "           ,'{7}' ";
                SQLString = SQLString + "           ,{8} ";
                SQLString = SQLString + "           ,'{9}' ";
                SQLString = SQLString + "           ,{10} ";
                SQLString = SQLString + "           ,'{11}' ";
                SQLString = SQLString + "           ,Case When '{12}'<>'01/01/0001 00:00:00' then '{12}' else null end); SELECT SCOPE_IDENTITY() ";

                if ((SolventeDetail.DataScadenza != null) && (SolventeDetail.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = SolventeDetail.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, SolventeDetail.IDSolventeMaster, SolventeDetail.IDSchedaDocumenti, SolventeDetail.IDSolvente, SolventeDetail.UM_Prelievo.Replace("'", "''"), SolventeDetail.Quantita_Prelievo.ToString().Replace(",",".").Replace("'", "''"), SolventeDetail.Note.Replace("'", "''"), SolventeDetail.IDApparecchio, SolventeDetail.IDUtensile, SolventeDetail.IDApparecchio2, SolventeDetail.IDUtensile2, SolventeDetail.Tipologia_MR, SolventeDetail.CAS.Replace("'", "''"), DataScadenza);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                newid = Convert.ToInt32(cmd.ExecuteScalar());

                EOS.Core.Control.Control_Solventi ControlSolventi = new Core.Control.Control_Solventi();
                ControlSolventi.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Solventi ModelSolventi = new Core.Model.Model_Solventi();
                ModelSolventi = ControlSolventi.GetSolventeByID(SolventeDetail.IDSolventeMaster).First().Value;
                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;
                ControlCalcolo.SetDataScadenza(SolventeDetail.IDSolventeMaster, ModelSolventi.DefaultGiorniScadenza, ModelSolventi.DataPreparazione, "Solvente");

                ModelSolventi = null;
                ControlSolventi = null;
                ControlCalcolo = null;

                cmd = null;
                cnn = null;

                AddLogSolventeDettaglio("Inserimento", SolventeDetail, DataScadenza,newid);

                return newid;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int UpdateSolventeDetail(Model_Solventi_Details SolventeDetail)
        {
            string DataScadenza;
            
            try
            {
                Model_Solventi_Details SolventeDetailOriginale= this.GetByIDSolventeDetail(SolventeDetail.IDSolventeDetail).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "UPDATE [dbo].[Solventi_Details] ";
                SQLString = SQLString + "   SET [IDSolventeMaster] = '{0}' ";
                SQLString = SQLString + "      ,[IDSchedaDocumenti] = '{1}' ";
                SQLString = SQLString + "      ,[IDSolvente] = '{2}' ";
                SQLString = SQLString + "      ,[UM_Prelievo] = '{3}' ";
                SQLString = SQLString + "      ,[Quantita_Prelievo] = {4} ";
                SQLString = SQLString + "      ,[Note] = '{5}' ";
                SQLString = SQLString + "      ,[IDApparecchio] = {6} ";
                SQLString = SQLString + "      ,[IDUtensile] = {7} ";
                SQLString = SQLString + "      ,[IDApparecchio2] = {8} ";
                SQLString = SQLString + "      ,[IDUtensile2] = {9} ";
                SQLString = SQLString + "      ,[Tipologia_MR] = {10} ";
                SQLString = SQLString + "      ,[CAS] = '{11}' ";
                SQLString = SQLString + "      ,[DataScadenza] = Case When '{12}'<>'01/01/0001 00:00:00' then '{12}' else null end ";
                SQLString = SQLString + " WHERE  IDSolventeDetail={13} ";

                if ((SolventeDetail.DataScadenza != null) && (SolventeDetail.DataScadenza != Convert.ToDateTime("01/01/0001 00:00:00")))
                {
                    DataScadenza = SolventeDetail.DataScadenza.ToString();
                }
                else
                {
                    DataScadenza = "01/01/0001 00:00:00";
                }

                SQLString = string.Format(SQLString, SolventeDetail.IDSolventeMaster, SolventeDetail.IDSchedaDocumenti, SolventeDetail.IDSolvente, SolventeDetail.UM_Prelievo.Replace("'", "''"), SolventeDetail.Quantita_Prelievo.ToString().Replace(",", ".").Replace("'", "''"), SolventeDetail.Note.Replace("'", "''"), SolventeDetail.IDApparecchio, SolventeDetail.IDUtensile, SolventeDetail.IDApparecchio2, SolventeDetail.IDUtensile2, SolventeDetail.Tipologia_MR, SolventeDetail.CAS.Replace("'", "''"), DataScadenza, SolventeDetail.IDSolventeDetail);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                AddLogSolventeDettaglio("Aggiornamento", SolventeDetail, DataScadenza, 0, SolventeDetailOriginale);

                SolventeDetailOriginale = null;

                EOS.Core.Control.Control_Solventi ControlSolventi = new Core.Control.Control_Solventi();
                ControlSolventi.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Solventi ModelSolventi = new Core.Model.Model_Solventi();
                ModelSolventi = ControlSolventi.GetSolventeByID(SolventeDetail.IDSolventeMaster).First().Value;
                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;
                ControlCalcolo.SetDataScadenza(SolventeDetail.IDSolventeMaster, ModelSolventi.DefaultGiorniScadenza, ModelSolventi.DataPreparazione, "Solvente");

                ControlSolventi.AddLogSolVente("Aggiornamento", ModelSolventi, ModelSolventi.DataPreparazione.ToString(), ModelSolventi.DataScadenza.ToString(), ModelSolventi.DataCreazione.ToString());

                ModelSolventi = null;
                ControlSolventi = null;
                ControlCalcolo = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public int AddLogSolventeDettaglio(string TipoOperazione, Model_Solventi_Details SolventeDetail, string DataScadenza, int newid = 0, Model_Solventi_Details SolventeDetailOriginale=null)
        {
            try
            {
                Control_Transcode ctlTranscode = new Control_Transcode();
                Control_Log ctlLog = new Control_Log();

                string SQLStringLog = "";

                if (TipoOperazione == "Cancellazione")
                {
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolventeMaster) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro (Componente) = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolvente) + System.Environment.NewLine;
                    SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SolventeDetail.IDSchedaDocumenti) + System.Environment.NewLine;
                }
                else
                {
                    if (TipoOperazione == "Inserimento")
                    {
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolventeMaster) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro (Componente) = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolvente) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SolventeDetail.IDSchedaDocumenti) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "TipologiaMR = " + ctlTranscode.GetTipologiaMRByID(SolventeDetail.Tipologia_MR) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "CAS = " + SolventeDetail.CAS + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "DataScadenza = " + DataScadenza + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "UMPrelievo = " + SolventeDetail.UM_Prelievo + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "QuantitaPrelievo = " + SolventeDetail.Quantita_Prelievo + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NumeroApparecchio = " + ctlTranscode.GetCodiceApparecchioByID(SolventeDetail.IDApparecchio) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NomeUtensile = " + ctlTranscode.GetNomeUtensileByID(SolventeDetail.IDUtensile) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NumeroApparecchio2 = " + ctlTranscode.GetCodiceApparecchioByID(SolventeDetail.IDApparecchio2) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "NomeUtensile2 = " + ctlTranscode.GetNomeUtensileByID(SolventeDetail.IDUtensile2) + System.Environment.NewLine;
                        SQLStringLog = SQLStringLog + "Note = " + SolventeDetail.Note;
                    }
                    else
                    {

                        try
                        {
                            if ((SolventeDetailOriginale.Tipologia_MR == null) || (SolventeDetailOriginale.Tipologia_MR == 0))
                            {
                                SolventeDetailOriginale.Tipologia_MR = 0;
                            }
                        }
                        catch (NullReferenceException e)
                        {
                            SolventeDetailOriginale.Tipologia_MR = 0;
                        }

                        SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolventeMaster) + System.Environment.NewLine;

                        SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro (Componente) = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolvente) + System.Environment.NewLine;

                        SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SolventeDetail.IDSchedaDocumenti) + System.Environment.NewLine;

                        if (SolventeDetail.Tipologia_MR != SolventeDetailOriginale.Tipologia_MR)
                        {
                            SQLStringLog = SQLStringLog + "TipologiaMR Precedente = " + ctlTranscode.GetTipologiaMRByID(SolventeDetailOriginale.Tipologia_MR) + " - Attuale = " + ctlTranscode.GetTipologiaMRByID(SolventeDetail.Tipologia_MR) + System.Environment.NewLine;
                        }

                        //if (SolventeDetail.IDSolvente != SolventeDetailOriginale.IDSolvente)
                        //{
                        //    SQLStringLog = SQLStringLog + "CodiceSoluzioneDiLavoro Precedente (Componente) = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetailOriginale.IDSolvente) + " - Attuale = " + ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolvente) + System.Environment.NewLine;
                        //}

                        //if (SolventeDetail.IDSchedaDocumenti != SolventeDetailOriginale.IDSchedaDocumenti)
                        //{
                        //    SQLStringLog = SQLStringLog + "Identificativo - LottoMateriale Precedente (Componente) = " + ctlTranscode.GetIdentificativoELottoByID(SolventeDetailOriginale.IDSchedaDocumenti) + " - Attuale = " + ctlTranscode.GetIdentificativoELottoByID(SolventeDetail.IDSchedaDocumenti) + System.Environment.NewLine;
                        //}

                        if (SolventeDetail.CAS != SolventeDetailOriginale.CAS)
                        {
                            SQLStringLog = SQLStringLog + "CAS Precedente = " + SolventeDetailOriginale.CAS + " - Attuale = " + SolventeDetail.CAS + System.Environment.NewLine;
                        }

                        if (SolventeDetail.DataScadenza != SolventeDetailOriginale.DataScadenza)
                        {
                            SQLStringLog = SQLStringLog + "DataScadenza Precedente  " + SolventeDetailOriginale.DataScadenza + " - Attuale = " + DataScadenza + System.Environment.NewLine;
                        }

                        if (SolventeDetail.UM_Prelievo != SolventeDetailOriginale.UM_Prelievo)
                        {
                            SQLStringLog = SQLStringLog + "UMPrelievo Precedente = " + SolventeDetailOriginale.UM_Prelievo + " - Attuale = " + SolventeDetail.UM_Prelievo + System.Environment.NewLine;
                        }

                        if (SolventeDetail.Quantita_Prelievo != SolventeDetailOriginale.Quantita_Prelievo)
                        {
                            SQLStringLog = SQLStringLog + "QuantitaPrelievo Precedente = " + SolventeDetailOriginale.Quantita_Prelievo + " - Attuale = " + SolventeDetail.Quantita_Prelievo + System.Environment.NewLine;
                        }

                        if (SolventeDetail.IDApparecchio != SolventeDetailOriginale.IDApparecchio)
                        {
                            SQLStringLog = SQLStringLog + "NumeroApparecchio Precedente = " + ctlTranscode.GetCodiceApparecchioByID(SolventeDetailOriginale.IDApparecchio) + " - Attuale = " + ctlTranscode.GetCodiceApparecchioByID(SolventeDetail.IDApparecchio) + System.Environment.NewLine;
                        }

                        if (SolventeDetail.IDUtensile != SolventeDetailOriginale.IDUtensile)
                        {
                            SQLStringLog = SQLStringLog + "NomeUtensile Precedente = " + ctlTranscode.GetNomeUtensileByID(SolventeDetailOriginale.IDUtensile) + " - Attuale = " + ctlTranscode.GetNomeUtensileByID(SolventeDetail.IDUtensile) + System.Environment.NewLine;
                        }

                        if (SolventeDetail.IDApparecchio2 != SolventeDetailOriginale.IDApparecchio2)
                        {
                            SQLStringLog = SQLStringLog + "NumeroApparecchio2 Precedente = " + ctlTranscode.GetCodiceApparecchioByID(SolventeDetailOriginale.IDApparecchio2) + " - Attuale = " + ctlTranscode.GetCodiceApparecchioByID(SolventeDetail.IDApparecchio2) + System.Environment.NewLine;
                        }

                        if (SolventeDetail.IDUtensile2 != SolventeDetailOriginale.IDUtensile2)
                        {
                            SQLStringLog = SQLStringLog + "NomeUtensile2 Precedente = " + ctlTranscode.GetNomeUtensileByID(SolventeDetailOriginale.IDUtensile2) + " - Attuale = " + ctlTranscode.GetNomeUtensileByID(SolventeDetail.IDUtensile2) + System.Environment.NewLine;
                        }

                        if (SolventeDetail.Note != SolventeDetailOriginale.Note)
                        {
                            SQLStringLog = SQLStringLog + "Note Precedente = " + SolventeDetailOriginale.Note + " - Attuale = " + SolventeDetail.Note;
                        }
                    }
                }

                ctlLog.ConnectionString = ConnectionString;

                if (newid != 0)
                {
                    ctlLog.InsertLog(TipoOperazione, "Soluzioni di Lavoro Dettaglio", newid, ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolventeMaster), SQLStringLog, IDUtente);
                }
                else
                {
                    ctlLog.InsertLog(TipoOperazione, "Soluzioni di Lavoro Dettaglio", SolventeDetail.IDSolventeDetail, ctlTranscode.GetCodiceSolventeByID(SolventeDetail.IDSolventeMaster), SQLStringLog, IDUtente);
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

        public int DeleteSolventeDetail(int IDSolventeDetail)
        {
            try
            {
                EOS.Core.Control.Control_Solventi_Details ControlSolventiDetails = new Core.Control.Control_Solventi_Details();
                ControlSolventiDetails.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Solventi_Details ModelSolventiDetails = new EOS.Core.Model.Model_Solventi_Details();
                ModelSolventiDetails = ControlSolventiDetails.GetByIDSolventeDetail(IDSolventeDetail).First().Value;

                System.Data.SqlClient.SqlConnection cnn = new System.Data.SqlClient.SqlConnection();
                cnn.ConnectionString = ConnectionString;
                cnn.Open();

                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                string SQLString = "";
                SQLString = SQLString + "DELETE FROM [dbo].[Solventi_Details] WHERE IDSolventeDetail={0} ";
                SQLString = string.Format(SQLString, IDSolventeDetail);

                cmd.CommandText = SQLString;
                cmd.Connection = cnn;
                cmd.ExecuteNonQuery();

                cmd = null;
                cnn = null;

                AddLogSolventeDettaglio("Cancellazione", ModelSolventiDetails, ModelSolventiDetails.DataScadenza.ToString());

                EOS.Core.Control.Control_Calcolo ControlCalcolo = new EOS.Core.Control.Control_Calcolo();
                ControlCalcolo.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                ControlCalcolo.IDUtente = IDUtente;
                EOS.Core.Control.Control_Solventi ControlSolventi = new Core.Control.Control_Solventi();
                ControlSolventi.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
                EOS.Core.Model.Model_Solventi ModelSolventi = new Core.Model.Model_Solventi();
                ModelSolventi = ControlSolventi.GetSolventeByID(ModelSolventiDetails.IDSolventeMaster).First().Value;

                ControlCalcolo.SetDataScadenza(ModelSolventiDetails.IDSolventeMaster, ModelSolventi.DefaultGiorniScadenza, ModelSolventi.DataPreparazione, "Solvente");

                ModelSolventi = null;
                ControlSolventi = null;
                ControlCalcolo = null;
                ModelSolventiDetails = null;
                ControlSolventiDetails = null;

                return 1;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public IDictionary<int, Model_Solventi_Details> GetData(string SQLString)
        {
            IDictionary<int, Model_Solventi_Details> ret = new Dictionary<int, Model_Solventi_Details>();

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
                            Model_Solventi_Details r = new Model_Solventi_Details();

                            if (DBNull.Value.Equals(dr["IDSolventeDetail"])) { r.IDSolventeDetail = 0; } else { r.IDSolventeDetail = Convert.ToInt32(dr["IDSolventeDetail"]); }
                            if (DBNull.Value.Equals(dr["IDSolventeMaster"])) { r.IDSolventeMaster = 0; } else { r.IDSolventeMaster = Convert.ToInt32(dr["IDSolventeMaster"]); }
                            if (DBNull.Value.Equals(dr["IDSchedaDocumenti"])) { r.IDSchedaDocumenti = 0; } else { r.IDSchedaDocumenti = Convert.ToInt32(dr["IDSchedaDocumenti"]); }
                            if (DBNull.Value.Equals(dr["IDSolvente"])) { r.IDSolvente = 0; } else { r.IDSolvente = Convert.ToInt32(dr["IDSolvente"]); }
                            if (DBNull.Value.Equals(dr["UM_Prelievo"])) { r.UM_Prelievo = ""; } else { r.UM_Prelievo = Convert.ToString(dr["UM_Prelievo"]); }
                            if (DBNull.Value.Equals(dr["Quantita_Prelievo"])) { r.Quantita_Prelievo = 0; } else { r.Quantita_Prelievo = Convert.ToDecimal(dr["Quantita_Prelievo"]); }
                            if (DBNull.Value.Equals(dr["Note"])) { r.Note = ""; } else { r.Note = Convert.ToString(dr["Note"]); }
                            if (DBNull.Value.Equals(dr["IDApparecchio"])) { r.IDApparecchio = 0; } else { r.IDApparecchio = Convert.ToInt32(dr["IDApparecchio"]); }
                            if (DBNull.Value.Equals(dr["IDUtensile"])) { r.IDUtensile = 0; } else { r.IDUtensile = Convert.ToInt32(dr["IDUtensile"]); }
                            if (DBNull.Value.Equals(dr["IDApparecchio2"])) { r.IDApparecchio2 = 0; } else { r.IDApparecchio2 = Convert.ToInt32(dr["IDApparecchio2"]); }
                            if (DBNull.Value.Equals(dr["IDUtensile2"])) { r.IDUtensile2 = 0; } else { r.IDUtensile2 = Convert.ToInt32(dr["IDUtensile2"]); }
                            if (DBNull.Value.Equals(dr["Tipologia_MR"])) { r.Tipologia_MR = 0; } else { r.Tipologia_MR = Convert.ToInt32(dr["Tipologia_MR"]); }
                            if (DBNull.Value.Equals(dr["CAS"])) { r.CAS = ""; } else { r.CAS = Convert.ToString(dr["CAS"]); }
                            if (DBNull.Value.Equals(dr["DataScadenza"])) { r.DataScadenza = Convert.ToDateTime("01/01/0001"); } else { r.DataScadenza = Convert.ToDateTime(dr["DataScadenza"]); }

                            ret.Add(r.IDSolventeDetail, r);
                            r = null;
                        }
                    }
                    else
                    {
                        Model_Solventi_Details r = new Model_Solventi_Details();

                        r.IDSolventeDetail = 0;
                        r.IDSolventeMaster = 0;
                        r.IDSchedaDocumenti = 0;
                        r.IDSolvente = 0;
                        r.UM_Prelievo = "";
                        r.Quantita_Prelievo = Convert.ToDecimal(0);
                        r.Note = "";
                        r.IDApparecchio = 0;
                        r.IDUtensile = 0;
                        r.IDApparecchio2 = 0;
                        r.IDUtensile2 = 0;
                        r.Tipologia_MR = 0;
                        r.CAS = "";
                        r.DataScadenza= Convert.ToDateTime(null);

                        ret.Add(r.IDSolventeDetail, r);
                        r = null;
                    }
                }
            }
            return ret;
        }
    }
}
