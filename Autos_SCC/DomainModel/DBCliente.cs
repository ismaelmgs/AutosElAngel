using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autos_SCC.Objetos;
using NucleoBase.Core;
using System.Data;

namespace Autos_SCC.DomainModel
{
    public class DBCliente : DBBase
    {
        public DataTable dtGetCotizacionesSucursal(int iIdSucursal)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCotizacionesSucursal]", "@fi_Sucursal", iIdSucursal);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable oGetCliente(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaCotizacion]", "@fi_Id", iId);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable dtGetEstados
        {
            get
            {
                try
                {
                    return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaEstados]");
                }
                catch
                {
                    return new DataTable();
                }
            }
        }

        public DataTable dtGetMunicipios(int iIdEstado)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaMunicipiosEstado]", "@ID_Estado", iIdEstado);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable dtGetColonias(int iIdEstado, string sDescripcion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaColoniasMunicipio]", "@ID_Estado",iIdEstado,
                                                                                        "@Des_Municipio", sDescripcion);
            }
            catch
            {
                return new DataTable();
            }
        }

        public DataTable dtGetCodigoPostal(int iIdEstado,string sMunicipio,string sDescripcion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Catalogos].[spS_ConsultaCodigoPosalColonia]", "@ID_Estado",iIdEstado,
                                                                                        "@Des_Municipio", sMunicipio,
                                                                                        "@Des_Colonia", sDescripcion);
            }
            catch
            {
                return new DataTable();
            }
        }

        public void DBSaveObj(ref Cliente oEjecut)
        {
            try
            {
                DataTable dt = new DataTable();

                oEjecut.oErr.bExisteError = false;
                oEjecut.oErr.sMsjError = "Guardado exitoso";

                if (oEjecut != null)
                {
                    if (DBGetObjPorCotizacion(oEjecut.iIdCotizacion) == null)
                    {
                        //Inserta
                        //if (DBExistsAuto(oEjecut.sPlaca) == null)
                        //{
                        DateTime dtFechaNac;

                        string [] sFecha = oEjecut.sFechaNac.Split('/');
                        if(sFecha.Length > 0)
                            dtFechaNac = new DateTime(sFecha[2].S().I(),sFecha[1].S().I(),sFecha[0].S().I());
                        else
                            dtFechaNac = DateTime.Now;

                        dt = oDB_SP.EjecutarDT("[Autos].[spI_InsertaCliente]", "@fi_IdCotizacion", oEjecut.iIdCotizacion,
                                                                                "@fi_IdCP", oEjecut.iIdCP,
                                                                                "@fc_Calle", oEjecut.sCalle,
                                                                                "@fi_NoExterior", oEjecut.iNoExt,
                                                                                "@fc_NoInterior", oEjecut.iNoInt,
                                                                                "@fi_Nacionalidad", oEjecut.iNacionalidad,
                                                                                "@fi_TipoIdentificacion", oEjecut.iTipoIdentificacion,
                                                                                "@fc_NoIFE", oEjecut.sNumeroIFE,
                                                                                "@fc_CURP", oEjecut.sCURP,
                                                                                "@fc_RCF", oEjecut.sRFC,
                                                                                "@fd_FechaNacimiento", dtFechaNac,
                                                                                "@fi_Sexo", oEjecut.iSexo,
                                                                                "@fi_EdoCivil", oEjecut.iEdoCivil,
                                                                                "@fc_Lada", oEjecut.sLada,
                                                                                "@fc_Telefono", oEjecut.sTelefono,
                                                                                "@fc_TelefonoCel", oEjecut.sTelCel,
                                                                                "@fi_TiempoVivirDom", oEjecut.iTiempoVivir,
                                                                                "@fi_Activo", oEjecut.iActivo,
                                                                                "@fc_Usuario", oEjecut.sUsuario);
                        //}
                        //else
                        //{
                        //    oEjecut.oErr.bExisteError = true;
                        //    oEjecut.oErr.sMsjError = "Ya existe otro Cliente con este nombre: " + oEjecut.sNombre + " " + oEjecut.sApePaterno + ", favor de verificar.";
                        //}
                    }
                    else
                    {
                        //Actualiza
                        DateTime dtFechaNac;

                        string[] sFecha = oEjecut.sFechaNac.Split('/');
                        if (sFecha.Length > 0)
                            dtFechaNac = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                        else
                            dtFechaNac = DateTime.Now;

                        dt = oDB_SP.EjecutarDT("[Autos].[spU_ActualizaCliente]", "@fi_Id", oEjecut.iId,
                                                                                "@fi_IdCotizacion", oEjecut.iIdCotizacion,
                                                                                "@fi_IdCP", oEjecut.iIdCP,
                                                                                "@fc_Calle", oEjecut.sCalle,
                                                                                "@fi_NoExterior", oEjecut.iNoExt,
                                                                                "@fc_NoInterior", oEjecut.iNoInt,
                                                                                "@fi_Nacionalidad", oEjecut.iNacionalidad,
                                                                                "@fi_TipoIdentificacion", oEjecut.iTipoIdentificacion,
                                                                                "@fc_NoIFE", oEjecut.sNumeroIFE,
                                                                                "@fc_CURP", oEjecut.sCURP,
                                                                                "@fc_RCF", oEjecut.sRFC,
                                                                                "@fd_FechaNacimiento", dtFechaNac,
                                                                                "@fi_Sexo", oEjecut.iSexo,
                                                                                "@fi_EdoCivil", oEjecut.iEdoCivil,
                                                                                "@fc_Lada", oEjecut.sLada,
                                                                                "@fc_Telefono", oEjecut.sTelefono,
                                                                                "@fc_TelefonoCel", oEjecut.sTelCel,
                                                                                "@fi_TiempoVivirDom", oEjecut.iTiempoVivir,
                                                                                "@fi_Activo", oEjecut.iActivo,
                                                                                "@fc_Usuario", oEjecut.sUsuario);
                    }
                    oEjecut.iId = dt != null && dt.Rows.Count > 0? dt.Rows[0][0].S().I() : -1;
                }

            }
            catch (Exception ex)
            {
                oEjecut.oErr.bExisteError = true;
                oEjecut.oErr.sMsjError = "ERROR al guardar (DBSaveObj) => " + ex.Message;
            }
        }

        public DataTable DBSaveObjAval(ref Cliente oEjecut)
        {
            DataTable dt = new DataTable();
            try
            {
                

                oEjecut.oErr.bExisteError = false;
                oEjecut.oErr.sMsjError = "Guardado exitoso";

                if (oEjecut != null)
                {
                    if (DBGetObjAvalPorCotizacion(oEjecut.iIdCotizacion) == null)
                    {
                        //Inserta
                        //if (DBExistsAuto(oEjecut.sPlaca) == null)
                        //{
                        DateTime dtFechaNac;

                        string[] sFecha = oEjecut.sFechaNac.Split('/');
                        if (sFecha.Length > 0)
                            dtFechaNac = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                        else
                            dtFechaNac = DateTime.Now;

                        dt = oDB_SP.EjecutarDT("[Autos].[spI_InsertaAval]", "@fi_IdCotizacion", oEjecut.iIdCotizacion,
                                                                            "@fc_Nombre",oEjecut.sNombre,
                                                                            "@fc_Nombre2",oEjecut.sNombre2,
                                                                            "@fc_ApePaterno",oEjecut.sApePaterno,
                                                                            "@fc_ApeMaterno",oEjecut.sApeMaterno,
                                                                            "@fi_IdCP", oEjecut.iIdCP,
                                                                            "@fc_Calle", oEjecut.sCalle,
                                                                            "@fi_NoExterior", oEjecut.iNoExt,
                                                                            "@fc_NoInterior", oEjecut.iNoInt,
                                                                            "@fi_Nacionalidad", oEjecut.iNacionalidad,
                                                                            "@fi_TipoIdentificacion", oEjecut.iTipoIdentificacion,
                                                                            "@fc_NoIFE", oEjecut.sNumeroIFE,
                                                                            "@fc_CURP", oEjecut.sCURP,
                                                                            "@fc_RCF", oEjecut.sRFC,
                                                                            "@fd_FechaNacimiento", dtFechaNac,
                                                                            "@fi_Sexo", oEjecut.iSexo,
                                                                            "@fi_EdoCivil", oEjecut.iEdoCivil,
                                                                            "@fc_Lada", oEjecut.sLada,
                                                                            "@fc_Telefono", oEjecut.sTelefono,
                                                                            "@fc_TelefonoCel", oEjecut.sTelCel,
                                                                            "@fi_TiempoVivirDom", oEjecut.iTiempoVivir,
                                                                            "@fi_Activo", oEjecut.iActivo,
                                                                            "@fc_Usuario", oEjecut.sUsuario);
                        //}
                        //else
                        //{
                        //    oEjecut.oErr.bExisteError = true;
                        //    oEjecut.oErr.sMsjError = "Ya existe otro Cliente con este nombre: " + oEjecut.sNombre + " " + oEjecut.sApePaterno + ", favor de verificar.";
                        //}
                        //return dt;
                    }
                    else
                    {
                        //Actualiza
                        DateTime dtFechaNac;

                        string[] sFecha = oEjecut.sFechaNac.Split('/');
                        if (sFecha.Length > 0)
                            dtFechaNac = new DateTime(sFecha[2].S().I(), sFecha[1].S().I(), sFecha[0].S().I());
                        else
                            dtFechaNac = DateTime.Now;

                        dt = oDB_SP.EjecutarDT("[Autos].[spU_ActualizaAval]",   "@fi_IdCotizacion", oEjecut.iIdCotizacion,
                                                                                "@fc_Nombre", oEjecut.sNombre,
                                                                                "@fc_Nombre2", oEjecut.sNombre2,
                                                                                "@fc_ApePaterno", oEjecut.sApePaterno,
                                                                                "@fc_ApeMaterno", oEjecut.sApeMaterno,
                                                                                "@fi_IdCP", oEjecut.iIdCP,
                                                                                "@fc_Calle", oEjecut.sCalle,
                                                                                "@fi_NoExterior", oEjecut.iNoExt,
                                                                                "@fc_NoInterior", oEjecut.iNoInt,
                                                                                "@fi_Nacionalidad", oEjecut.iNacionalidad,
                                                                                "@fi_TipoIdentificacion", oEjecut.iTipoIdentificacion,
                                                                                "@fc_NoIFE", oEjecut.sNumeroIFE,
                                                                                "@fc_CURP", oEjecut.sCURP,
                                                                                "@fc_RCF", oEjecut.sRFC,
                                                                                "@fd_FechaNacimiento", dtFechaNac,
                                                                                "@fi_Sexo", oEjecut.iSexo,
                                                                                "@fi_EdoCivil", oEjecut.iEdoCivil,
                                                                                "@fc_Lada", oEjecut.sLada,
                                                                                "@fc_Telefono", oEjecut.sTelefono,
                                                                                "@fc_TelefonoCel", oEjecut.sTelCel,
                                                                                "@fi_TiempoVivirDom", oEjecut.iTiempoVivir,
                                                                                "@fi_Activo", oEjecut.iActivo,
                                                                                "@fc_Usuario", oEjecut.sUsuario);
                        //return dt;
                    }
                    oEjecut.iId = dt != null && dt.Rows.Count > 0 ? dt.Rows[0][0].S().I() : -1;
                    
                }
                
            }
            catch (Exception ex)
            {
                oEjecut.oErr.bExisteError = true;
                oEjecut.oErr.sMsjError = "ERROR al guardar (DBSaveObj) => " + ex.Message;
            }
            return dt;
        }

        public Cliente DBGetObj(int iId)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaClienteId]", "@fi_Id", iId).AsEnumerable().Select(r => new Cliente()
                {
                    iId = r["fi_Id"].S().I(),
                    iIdCotizacion = r["fi_IdCotizacion"].S().I(),
                    iIdCP = r["fi_IdCP"].S().I(),
                    iEstado = r["ID_Estado"].S().I(),
                    sMunicipio = r["Des_Municipio"].S(),
                    sColonia = r["Des_Colonia"].S(),
                    sCP = r["Des_CodigoPostal"].S(),
                    sCalle = r["fc_Calle"].S(),
                    iNoExt = r["fi_NoExterior"].S().I(),
                    iNoInt = r["fc_NoInterior"].S().I(),
                    iNacionalidad = r["fi_Nacionalidad"].S().I(),
                    iTipoIdentificacion = r["fi_TipoIdentificacion"].S().I(),
                    sNumeroIFE = r["fc_NoIFE"].S(),
                    sCURP = r["fc_CURP"].S(),
                    sRFC = r["fc_RCF"].S(),
                    dtFechaNacimiento = r["fd_FechaNacimiento"].S().Dt(),
                    iSexo = r["fi_Sexo"].S().I(),
                    iEdoCivil = r["fi_EdoCivil"].S().I(),
                    sLada = r["fc_Lada"].S(),
                    sTelefono = r["fc_Telefono"].S(),
                    sTelCel = r["fc_TelefonoCel"].S(),
                    iTiempoVivir = r["fi_TiempoVivirDom"].S().I(),
                    iActivo = r["fi_Activo"].S().I(),
                    sUsuario = r["fc_Usuario"].S(),
                    dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                    sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                }).First();
            }
            catch
            {
                return null;
            }
        }

        public Cliente DBGetObjPorCotizacion(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaClienteCotizacion]", "@fi_IdCotizacion", iIdCotizacion).AsEnumerable().Select(r => new Cliente()
                {
                    iId = r["fi_Id"].S().I(),
                    iIdCotizacion = r["fi_IdCotizacion"].S().I(),
                    iIdCP = r["fi_IdCP"].S().I(),
                    iEstado = r["ID_Estado"].S().I(),
                    sMunicipio = r["Des_Municipio"].S(),
                    sColonia = r["Des_Colonia"].S(),
                    sCP = r["Des_CodigoPostal"].S(),
                    sCalle = r["fc_Calle"].S(),
                    iNoExt = r["fi_NoExterior"].S().I(),
                    iNoInt = r["fc_NoInterior"].S().I(),
                    iNacionalidad = r["fi_Nacionalidad"].S().I(),
                    iTipoIdentificacion = r["fi_TipoIdentificacion"].S().I(),
                    sNumeroIFE = r["fc_NoIFE"].S(),
                    sCURP = r["fc_CURP"].S(),
                    sRFC = r["fc_RCF"].S(),
                    dtFechaNacimiento = r["fd_FechaNacimiento"].S().Dt(),
                    iSexo = r["fi_Sexo"].S().I(),
                    iEdoCivil = r["fi_EdoCivil"].S().I(),
                    sLada = r["fc_Lada"].S(),
                    sTelefono = r["fc_Telefono"].S(),
                    sTelCel = r["fc_TelefonoCel"].S(),
                    iTiempoVivir = r["fi_TiempoVivirDom"].S().I(),
                    iActivo = r["fi_Activo"].S().I(),
                    sUsuario = r["fc_Usuario"].S(),
                    dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                    sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                }).First();
            }
            catch
            {
                return null;
            }
        }

        public DataTable DBGetTablaAvalPorCotizacion(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaAvalCotizacion]", "@fi_IdCotizacion", iIdCotizacion);
            }
            catch
            {
                return new DataTable();
            }
        }

        public Cliente DBGetObjAvalPorCotizacion(int iIdCotizacion)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Autos].[spS_ConsultaAvalCotizacion]", "@fi_IdCotizacion", iIdCotizacion).AsEnumerable().Select(r => new Cliente()
                {
                    iId = r["fi_Id"].S().I(),
                    iIdCotizacion = r["fi_IdCotizacion"].S().I(),
                    sNombre = r["fc_Nombre"].S(),
                    sNombre2 = r["fc_Nombre2"].S(),
                    sApePaterno = r["fc_ApePaterno"].S(),
                    sApeMaterno = r["fc_ApeMaterno"].S(),
                    iIdCP = r["fi_IdCP"].S().I(),
                    iEstado = r["ID_Estado"].S().I(),
                    sMunicipio = r["Des_Municipio"].S(),
                    sColonia = r["Des_Colonia"].S(),
                    sCP = r["Des_CodigoPostal"].S(),
                    sCalle = r["fc_Calle"].S(),
                    iNoExt = r["fi_NoExterior"].S().I(),
                    iNoInt = r["fc_NoInterior"].S().I(),
                    iNacionalidad = r["fi_Nacionalidad"].S().I(),
                    iTipoIdentificacion = r["fi_TipoIdentificacion"].S().I(),
                    sNumeroIFE = r["fc_NoIFE"].S(),
                    sCURP = r["fc_CURP"].S(),
                    sRFC = r["fc_RCF"].S(),
                    dtFechaNacimiento = r["fd_FechaNacimiento"].S().Dt(),
                    iSexo = r["fi_Sexo"].S().I(),
                    iEdoCivil = r["fi_EdoCivil"].S().I(),
                    sLada = r["fc_Lada"].S(),
                    sTelefono = r["fc_Telefono"].S(),
                    sTelCel = r["fc_TelefonoCel"].S(),
                    iTiempoVivir = r["fi_TiempoVivirDom"].S().I(),
                    iActivo = r["fi_Activo"].S().I(),
                    sUsuario = r["fc_Usuario"].S(),
                    dtFechaUltMov = r["fd_FechaUltMovimiento"].Dt(),
                    sFechaUltMov = r["fd_FechaUltMovimiento"].Dt().Day + "/" + r["fd_FechaUltMovimiento"].Dt().Month + "/" + r["fd_FechaUltMovimiento"].Dt().Year
                }).First();
            }
            catch
            {
                return null;
            }
        }

    }
}