using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Autos_SCC.Presenter;
using Autos_SCC.DomainModel;
using Autos_SCC.Interfaces;
using System.Data;
using NucleoBase.Core;
using Autos_SCC.Objetos;
using Autos_SCC.Clases;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace Autos_SCC.Views.Principales
{
    public partial class frmClientes : System.Web.UI.Page, IViewCliente
    {
        #region EVENTOS
        protected void Page_Load(object sender, EventArgs e)
        {
            oPresenter = new Cliente_Presenter(this, new DBCliente());

            if (!IsPostBack)
            {
                //Response.Expires = 0;

                if (Session["usuario"] == null)
                {
                    Response.Redirect("..//Default.aspx");
                }

                oPresenter.LoadObjects_Presenter();
            }

            if (Request[txtFechaNacimiento.UniqueID] != null)
            {
                if (Request[txtFechaNacimiento.UniqueID].Length > 0)
                {
                    txtFechaNacimiento.Text = Request[txtFechaNacimiento.UniqueID];
                }
            }

            if (Request[txtFechaNacimientoAval.UniqueID] != null)
            {
                if (Request[txtFechaNacimientoAval.UniqueID].Length > 0)
                {
                    txtFechaNacimientoAval.Text = Request[txtFechaNacimientoAval.UniqueID];
                }
            }
        }

        protected void ddlSucursal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetCotizaciones != null)
                eGetCotizaciones(sender, e);

            
            ddlCotizacion.DataSource = dtCotizacion;
            ddlCotizacion.DataTextField = "NombreCompleto";
            ddlCotizacion.DataValueField = "fi_Id";
            ddlCotizacion.DataBind();

            if(dtCotizacion.Rows.Count > 0)
            {
                ddlCotizacion.Items.Insert(dtCotizacion.Rows.Count, new ListItem("Selecciona", "0"));
                ddlCotizacion.SelectedIndex = dtCotizacion.Rows.Count;
            }
           
            //ddlCotizacion_SelectedIndexChanged(null, EventArgs.Empty);
        }

        protected void ddlCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            LimpiaCampos();
            lblMensajeResultados.Visible = false;

            if (eGetCliente != null)
                eGetCliente(sender, e);

            if (dtCliente.Rows.Count > 0)
            {
                HidIdCliente.Value = dtCliente.Rows[0]["fi_IdCliente"].S();
                lblCliente.Text = "Cliente: " + dtCliente.Rows[0]["NombreCompleto"].S();
                lblTipoAuto.Text = "Tipo auto: " + dtCliente.Rows[0]["fc_TipoAuto"].S();
                lblPrecio.Text = "Precio: $ " + Math.Round(dtCliente.Rows[0]["fm_Precio"].S().D(), 0).S();
                lblPlazo.Text = "Plazo: " + dtCliente.Rows[0]["fc_Plazo"].S();

                if (HidIdCliente.Value.S() == "0")
                    btnCapAval.Enabled = false;
                else
                    btnCapAval.Enabled = true;

                pnDatosClientes.Visible = true;
                pnDatosAval.Visible = true;
                pnDatosClienteSleecionado.Visible = true;
                upaDatos.Update();
            }
            else
            {
                lblMensajeResultados.Visible = true;
                pnDatosClientes.Visible = false;
                pnDatosAval.Visible = false;
                pnDatosClienteSleecionado.Visible = false;
                upaDatos.Update();
            }
        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(eGetMunicipio != null)
                eGetMunicipio(sender, e);

            if (dtMunicipios.Rows.Count > 0)
            {
                ddlMunicipio.DataSource = dtMunicipios;
                ddlMunicipio.DataValueField = "fc_Descripcion";
                ddlMunicipio.DataTextField = "fc_Descripcion";
                ddlMunicipio.DataBind();
            }

            ddlMunicipio_SelectedIndexChanged(sender, e);
        }

        protected void ddlMunicipio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetColonias != null)
                eGetColonias(sender, e);

            if (dtColonias.Rows.Count > 0)
            {
                ddlColonia.DataSource = dtColonias;
                ddlColonia.DataValueField = "fc_Descripcion";
                ddlColonia.DataTextField = "fc_Descripcion";
                ddlColonia.DataBind();
            }

            ddlColonia_SelectedIndexChanged(sender, e);
        }

        protected void ddlColonia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetCodigoP != null)
                eGetCodigoP(sender, e);

            if (dtCP.Rows.Count > 0)
            {
                txtCP.Text = dtCP.Rows[0]["fc_Descripcion"].S();
                HidIdCP.Value = dtCP.Rows[0]["fi_Id"].S();
            }
        }

        protected void btnCapAval_Click(object sender, EventArgs e)
        {
            try
            {
                if (eGetAval != null)
                    eGetAval(sender, e);

                mpeCapturaAval.Show();
            }
            catch (Exception ex)
            {
                MostrarMensaje("Se produjo el siguiente error al capturar: " + ex.Message, "Error al capturar aval");
            }
        }

        protected void ddlEstadoAval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetMunicipioAval != null)
                eGetMunicipioAval(sender, e);

            if (dtMunicipios.Rows.Count > 0)
            {
                ddlMunicipioAval.DataSource = dtMunicipios;
                ddlMunicipioAval.DataValueField = "fc_Descripcion";
                ddlMunicipioAval.DataTextField = "fc_Descripcion";
                ddlMunicipioAval.DataBind();
            }

            mpeCapturaAval.Show();
            ddlMunicipioAval_SelectedIndexChanged(sender, e);
        }

        protected void ddlMunicipioAval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetColoniasAval != null)
                eGetColoniasAval(sender, e);

            if (dtColonias.Rows.Count > 0)
            {
                ddlColoniaAval.DataSource = dtColonias;
                ddlColoniaAval.DataValueField = "fc_Descripcion";
                ddlColoniaAval.DataTextField = "fc_Descripcion";
                ddlColoniaAval.DataBind();
            }

            mpeCapturaAval.Show();
            ddlColoniaAval_SelectedIndexChanged(sender, e);
        }

        protected void ddlColoniaAval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (eGetCodigoPAval != null)
                eGetCodigoPAval(sender, e);

            if (dtCP.Rows.Count > 0)
            {
                txtCPAval.Text = dtCP.Rows[0]["fc_Descripcion"].S();
                HidIdCPAval.Value = dtCP.Rows[0]["fi_Id"].S();
            }

            mpeCapturaAval.Show();
        }

        protected void btnCancelarCapturaAval_Click(object sender, EventArgs e)
        {
            LimpiaCamposAval();
            mpeCapturaAval.Hide();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ddlCotizacion.SelectedValue.S().I() > 0)
            {
                if (eSaveObj != null)
                    eSaveObj(sender, e);

                if (bEsCorrectoCliente)
                    btnCapAval.Enabled = true;
            }
            else
                MostrarMensaje("Es necesario escoger un cliente para capturar sus datos", "Captura de campos");
        }

        protected void ddlTipoIdentificacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoIdentificacion.SelectedValue.S() == "0")
                lblNumeroIfe.Text = "Numero de IFE:";
            if (ddlTipoIdentificacion.SelectedValue.S() == "1")
                lblNumeroIfe.Text = "Numero de Licencia:";
            if (ddlTipoIdentificacion.SelectedValue.S() == "2")
                lblNumeroIfe.Text = "Numero de Pasaporte:";
            if (ddlTipoIdentificacion.SelectedValue.S() == "3")
                lblNumeroIfe.Text = "Numero de Otro:";
        }

        protected void ddlTipoIdentificacionAval_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTipoIdentificacionAval.SelectedValue.S() == "0")
                lblNoIfeAval.Text = "Número de IFE:";
            if (ddlTipoIdentificacionAval.SelectedValue.S() == "1")
                lblNoIfeAval.Text = "Número de Licencia:";
            if (ddlTipoIdentificacionAval.SelectedValue.S() == "2")
                lblNoIfeAval.Text = "Número de Pasaporte:";
            if (ddlTipoIdentificacionAval.SelectedValue.S() == "3")
                lblNoIfeAval.Text = "Número de Otro:";

            mpeCapturaAval.Show();
        }

        protected void btnGuardarAval_Click(object sender, EventArgs e)
        {
            if (ddlCotizacion.SelectedValue.S().I() > 0)
            {
                if (eSaveAval != null)
                    eSaveAval(sender, e);

                if (dtAvalSaved != null)
                {
                    gvAval.DataSource = dtAvalSaved;
                    gvAval.DataBind();
                }

                if (bEsCorrectoAval)
                    btnAceptarFormalizar.Enabled = true;
            }
            else
                MostrarMensaje("Es necesario escoger un cliente para capturar su aval", "Captura de campos");
        }

        protected void btnAceptarFormalizar_Click(object sender, EventArgs e)
        {
            if (dtAvalSaved != null)
            {
                Utils.CambiaEstatus(ddlCotizacion.SelectedValue.S().I(), Autos_SCC.Clases.Enumeraciones.eEstatus.Formalizar);
                Response.Redirect("frmFormalizacion.aspx");
            }
        }

        #endregion

        #region METODOS
        public void LoadSucursales(DataTable dtSuc)
        {
            if (dtSuc.Rows.Count > 0)
            {
                ddlSucursal.DataSource = dtSuc;
                ddlSucursal.DataValueField = "fi_Id";
                ddlSucursal.DataTextField = "fc_Descripcion";
                ddlSucursal.DataBind();
            }
        }

        public void LoadEstados(DataTable dtEst)
        {
            if (dtEst.Rows.Count > 0)
            {
                ddlEstado.DataSource = dtEst;
                ddlEstado.DataValueField = "fi_Id";
                ddlEstado.DataTextField = "fc_Descripcion";
                ddlEstado.DataBind();

                ddlEstadoAval.DataSource = dtEst;
                ddlEstadoAval.DataValueField = "fi_Id";
                ddlEstadoAval.DataTextField = "fc_Descripcion";
                ddlEstadoAval.DataBind();
            }
        }

        public void MostrarMensaje(string sMensaje, string sCaption)
        {
            omb2.ShowMessage(sMensaje, sCaption);
            //string script = string.Format("MostrarMensaje('{0}', '{1}')", sMensaje, sCaption);
            //ScriptManager.RegisterStartupScript(this, typeof(Page), "MostrarMensaje", script, true);
        }

        public void LimpiaCampos()
        {
            ddlEstado.SelectedIndex = 0;
            ddlMunicipio.Items.Clear();
            ddlColonia.Items.Clear();
            txtCP.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtNumExt.Text = string.Empty;
            txtNumInt.Text = string.Empty;
            ddlNacionalidad.SelectedIndex = 0;
            ddlTipoIdentificacion.SelectedIndex = 0;
            txtNumeroIfe.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtCURP.Text = string.Empty;
            ddlSexo.SelectedIndex = 0;
            ddlEstadoCivil.SelectedIndex = 0;
            txtLada.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTelefonoCel.Text = string.Empty;
            ddlTiempoVivirDomicilio.SelectedIndex = 0;
            
            gvAval.DataSource = null;
            gvAval.DataBind();
        }

        public void LimpiaCamposAval()
        {
            ddlEstadoAval.SelectedIndex = 0;
            ddlMunicipioAval.Items.Clear();
            ddlColoniaAval.Items.Clear();
            txtCPAval.Text = string.Empty;
            txtCalleAval.Text = string.Empty;
            txtNoExteriorAval.Text = string.Empty;
            txtNoInteriorAval.Text = string.Empty;
            ddlNacionalidadAval.SelectedIndex = 0;
            ddlTipoIdentificacionAval.SelectedIndex = 0;
            txtNoIfeAval.Text = string.Empty;
            txtFechaNacimientoAval.Text = string.Empty;
            txtRFCAval.Text = string.Empty;
            txtCURPAval.Text = string.Empty;
            ddlSexoAval.SelectedIndex = 0;
            ddlEstadoCivilAval.SelectedIndex = 0;
            txtLadaAval.Text = string.Empty;
            txtTelefonoAval.Text = string.Empty;
            txtTelefonoCelAval.Text = string.Empty;
            ddlTiempoVivirDomAval.SelectedIndex = 0;
        }

        public void CargaAval(DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                gvAval.DataSource = dt;
                gvAval.DataBind();

                btnAceptarFormalizar.Enabled = true;
            }
            else
                btnAceptarFormalizar.Enabled = false;
        }

        #endregion
        
        #region "Vars y Propiedades"
        Cliente_Presenter oPresenter;
        public event EventHandler eNewObj;
        public event EventHandler eObjSelected;
        public event EventHandler eSaveObj;
        public event EventHandler eDeleteObj;
        public event EventHandler eSearchObj;
        public event EventHandler eGetCotizaciones;
        public event EventHandler eGetCliente;
        public event EventHandler eGetMunicipio;
        public event EventHandler eGetColonias;
        public event EventHandler eGetCodigoP;

        public event EventHandler eGetMunicipioAval;
        public event EventHandler eGetColoniasAval;
        public event EventHandler eGetCodigoPAval;
        public event EventHandler eSaveAval;
        public event EventHandler eGetAval;
        public event EventHandler eSaveFormalizacion;


        public int iIdSucursal
        {
            get
            {
                return ddlSucursal.SelectedValue.S().I();
            }
        }

        public int iIdCotizacion
        {
            get
            {
                return ddlCotizacion.SelectedValue.S().I();
            }
        }

        public int iIdUsuario
        {
            get
            {
                int iIdUsu = 0;
                if (Session["oUserData"] != null)
                {
                    DataUserIndetity oUser = (DataUserIndetity)Session["oUserData"];
                    iIdUsu = oUser.IIdUsuario;
                }
                return iIdUsu;
            }
        }

        public DataTable dtCotizacion
        {
            set;
            get;
        }

        public DataTable dtAvalSaved
        {
            get { return (DataTable)ViewState["VdtAvalSaved"]; }
            set { ViewState["VdtAvalSaved"] = value; }
        }

        public DataTable dtCliente
        {
            get { return (DataTable)ViewState["dtClienteV"]; }
            set { ViewState["dtClienteV"] = value; }
        }

        public DataTable dtMunicipios
        {
            get { return (DataTable)ViewState["dtMunicipios1"]; }
            set { ViewState["dtMunicipios1"] = value; }
        }

        public DataTable dtColonias
        {
            get { return (DataTable)ViewState["dtColonias1"]; }
            set { ViewState["dtColonias1"] = value; }
        }

        public DataTable dtCP
        {
            get { return (DataTable)ViewState["dtCP1"]; }
            set { ViewState["dtCP1"] = value; }
        }

        public Direcciones oDireccion
        {
            get 
            {
                return new Direcciones()
                {
                    iIdEstado = ddlEstado.SelectedValue.S().I(),
                    sMunicipio = ddlMunicipio.SelectedValue.S(),
                    sColonia = ddlColonia.SelectedValue.S()
                };
            }
        }

        public Direcciones oDireccionAval
        {
            get
            {
                return new Direcciones()
                {
                    iIdEstado = ddlEstadoAval.SelectedValue.S().I(),
                    sMunicipio = ddlMunicipioAval.SelectedValue.S(),
                    sColonia = ddlColoniaAval.SelectedValue.S()
                };
            }
        }

        public Cliente oCliente
        {
            get
            {
                return new Cliente
                {
                    iIdCotizacion = ddlCotizacion.SelectedValue.S().I(),
                    iEstado = ddlEstado.SelectedValue.S().I(),
                    sMunicipio = ddlMunicipio.SelectedValue.S(),
                    sColonia = ddlColonia.SelectedValue.S(),
                    iIdCP = HidIdCP.Value.S().I(),
                    sCalle = txtCalle.Text.S(),
                    iNoExt = txtNumExt.Text.S().I(),
                    iNoInt = txtNumInt.Text.S().I(),
                    sCP = txtCP.Text,
                    iNacionalidad = ddlNacionalidad.SelectedValue.S().I(),
                    iTipoIdentificacion = ddlTipoIdentificacion.SelectedValue.S().I(),
                    sNumeroIFE = txtNumeroIfe.Text.S(),
                    sCURP = txtCURP.Text.S(),
                    sRFC = txtRFC.Text.S(),
                    sFechaNac = txtFechaNacimiento.Text.S(),
                    iSexo = ddlSexo.SelectedValue.S().I(),
                    iEdoCivil = ddlEstadoCivil.SelectedValue.S().I(),
                    sLada = txtLada.Text.S(),
                    sTelefono = txtTelefono.Text.S(),
                    sTelCel = txtTelefonoCel.Text.S(),
                    iTiempoVivir = ddlTiempoVivirDomicilio.SelectedValue.S().I(),
                    iActivo = 1,
                    sUsuario = Session["usuario"].S()
                };
            }
            set
            {
                Cliente oCat = value as Cliente;
                if (oCat != null)
                {
                    ddlEstado.SelectedValue = oCat.iEstado.S();
                    ddlEstado_SelectedIndexChanged(null, EventArgs.Empty);
                    ddlMunicipio.SelectedValue = oCat.sMunicipio;
                    ddlMunicipio_SelectedIndexChanged(null, EventArgs.Empty);
                    ddlColonia.SelectedValue = oCat.sColonia;
                    HidIdCP.Value = oCat.iIdCP.S();
                    txtCP.Text = oCat.sCP;
                    txtCalle.Text = oCat.sCalle.S();
                    txtNumExt.Text = oCat.iNoExt.S();
                    txtNumInt.Text = oCat.iNoInt.S();
                    ddlNacionalidad.SelectedValue = oCat.iNacionalidad.S();
                    ddlTipoIdentificacion.SelectedValue = oCat.iTipoIdentificacion.S();
                    txtNumeroIfe.Text = oCat.sNumeroIFE;
                    txtCURP.Text = oCat.sCURP;
                    txtRFC.Text = oCat.sRFC;
                    txtFechaNacimiento.Text = oCat.dtFechaNacimiento.S();
                    ddlSexo.SelectedValue = oCat.iSexo.S();
                    ddlEstadoCivil.SelectedValue = oCat.iEdoCivil.S();
                    txtLada.Text = oCat.sLada;
                    txtTelefono.Text = oCat.sTelefono;
                    txtTelefonoCel.Text = oCat.sTelCel;
                    ddlTiempoVivirDomicilio.SelectedValue = oCat.iTiempoVivir.S();
                }
            }
        }

        public Aval oAval
        {
            get
            {
                return new Aval
                {
                    iIdCotizacion = ddlCotizacion.SelectedValue.S().I(),
                    iEstado = ddlEstadoAval.SelectedValue.S().I(),
                    sMunicipio = ddlMunicipioAval.SelectedValue.S(),
                    sColonia = ddlColoniaAval.SelectedValue.S(),
                    sNombre = txtNombreAval.Text.S(),
                    sNombre2 = txtSegNombreAval.Text.S(),
                    sApePaterno = txtApePaternoAval.Text.S(),
                    sApeMaterno = txtApeMaternoAval.Text.S(),
                    iIdCP = HidIdCPAval.Value.S().I(),
                    sCP = txtCPAval.Text,
                    sCalle = txtCalleAval.Text.S(),
                    iNoExt = txtNoExteriorAval.Text.S().I(),
                    iNoInt = txtNoInteriorAval.Text.S().I(),
                    iNacionalidad = ddlNacionalidadAval.SelectedValue.S().I(),
                    iTipoIdentificacion = ddlTipoIdentificacionAval.SelectedValue.S().I(),
                    sNumeroIFE = txtNoIfeAval.Text.S(),
                    sCURP = txtCURPAval.Text.S(),
                    sRFC = txtRFCAval.Text.S(),
                    sFechaNac = txtFechaNacimientoAval.Text.S(),
                    iSexo = ddlSexoAval.SelectedValue.S().I(),
                    iEdoCivil = ddlEstadoCivilAval.SelectedValue.S().I(),
                    sLada = txtLadaAval.Text.S(),
                    sTelefono = txtTelefonoAval.Text.S(),
                    sTelCel = txtTelefonoCelAval.Text.S(),
                    iTiempoVivir = ddlTiempoVivirDomAval.SelectedValue.S().I(),
                    iActivo = 1,
                    sUsuario = Session["usuario"].S()
                };
            }
            set
            {
                Aval oCat = value as Aval;
                if (oCat != null)
                {
                    txtNombreAval.Text = oCat.sNombre;
                    txtSegNombreAval.Text = oCat.sNombre2;
                    txtApePaternoAval.Text = oCat.sApePaterno;
                    txtApeMaternoAval.Text = oCat.sApeMaterno;
                    ddlEstadoAval.SelectedValue = oCat.iEstado.S();
                    ddlEstadoAval_SelectedIndexChanged(null, EventArgs.Empty);
                    ddlMunicipioAval.SelectedValue = oCat.sMunicipio;
                    ddlMunicipioAval_SelectedIndexChanged(null, EventArgs.Empty);
                    ddlColoniaAval.SelectedValue = oCat.sColonia;
                    HidIdCPAval.Value = oCat.iIdCP.S();
                    txtCPAval.Text = oCat.sCP;
                    txtCalleAval.Text = oCat.sCalle.S();
                    txtNoExteriorAval.Text = oCat.iNoExt.S();
                    txtNoInteriorAval.Text = oCat.iNoInt.S();
                    ddlNacionalidadAval.SelectedValue = oCat.iNacionalidad.S();
                    ddlTipoIdentificacionAval.SelectedValue = oCat.iTipoIdentificacion.S();
                    txtNoIfeAval.Text = oCat.sNumeroIFE;
                    txtCURPAval.Text = oCat.sCURP;
                    txtRFCAval.Text = oCat.sRFC;
                    txtFechaNacimientoAval.Text = oCat.dtFechaNacimiento.S();
                    ddlSexoAval.SelectedValue = oCat.iSexo.S();
                    ddlEstadoCivilAval.SelectedValue = oCat.iEdoCivil.S();
                    txtLadaAval.Text = oCat.sLada;
                    txtTelefonoAval.Text = oCat.sTelefono;
                    txtTelefonoCelAval.Text = oCat.sTelCel;
                    ddlTiempoVivirDomAval.SelectedValue = oCat.iTiempoVivir.S();
                }
            }
        }

        private bool _bEsCorrectoAval = false;
        public bool bEsCorrectoAval
        {
            get { return _bEsCorrectoAval; }
            set { _bEsCorrectoAval = value; }
        }

        private bool _bEsCorrectoCliente = false;
        public bool bEsCorrectoCliente
        {
            get { return _bEsCorrectoCliente; }
            set { _bEsCorrectoCliente = value; }
        }

        private int _iOpcPagosInd = 0;
        //
        #endregion
    }
}