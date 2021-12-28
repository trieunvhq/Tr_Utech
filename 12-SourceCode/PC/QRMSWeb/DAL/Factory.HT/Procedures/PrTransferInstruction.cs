///////////////////////////////////////////////////////////////////////////
// Description: Data Access class for the table 'TransferInstruction'
// Generated by LLBLGen v1.3.5996.26197 Final on: Tuesday, December 28, 2021, 9:39:21 AM
// Because the Base Class already implements IDispose, this class doesn't.
///////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Data.SqlTypes;
using System.Data.SqlClient;

namespace DAL
{
	/// <summary>
	/// Purpose: Data Access class for the table 'TransferInstruction'.
	/// </summary>
	public class PrTransferInstruction : PrDBInteractionBase
	{
		#region Class Member Declarations
			private SqlString		m_sTransferType, m_sWarehouseType_To, m_sRecordStatus;
			private SqlDateTime		m_daCreateDate, m_daInstructionDate, m_daUpdateDate;
			private SqlInt32		m_iID;
			private SqlString		m_sWarehouseName_To, m_sWarehouseCode_To, m_sUserUpdate, m_sRemark, m_sUserCreate, m_sLocationCode_From, m_sLocationName_From, m_sTransferStatus, m_sTransferOrderNo, m_sGetDataStatus, m_sLocationCode_To, m_sLocationName_To, m_sWarehouseType_From, m_sWarehouseCode_From, m_sWarehouseName_From;
		#endregion


		/// <summary>
		/// Purpose: Class constructor.
		/// </summary>
		public PrTransferInstruction()
		{
			// Nothing for now.
		}


		/// <summary>
		/// Purpose: Insert method. This method will insert one new row into the database.
		/// </summary>
		/// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>sTransferOrderNo</LI>
		///		 <LI>daInstructionDate. May be SqlDateTime.Null</LI>
		///		 <LI>sGetDataStatus</LI>
		///		 <LI>sTransferStatus</LI>
		///		 <LI>sLocationCode_From. May be SqlString.Null</LI>
		///		 <LI>sLocationName_From. May be SqlString.Null</LI>
		///		 <LI>sWarehouseCode_From. May be SqlString.Null</LI>
		///		 <LI>sWarehouseName_From. May be SqlString.Null</LI>
		///		 <LI>sWarehouseType_From. May be SqlString.Null</LI>
		///		 <LI>sLocationCode_To. May be SqlString.Null</LI>
		///		 <LI>sLocationName_To. May be SqlString.Null</LI>
		///		 <LI>sWarehouseCode_To. May be SqlString.Null</LI>
		///		 <LI>sWarehouseName_To. May be SqlString.Null</LI>
		///		 <LI>sWarehouseType_To. May be SqlString.Null</LI>
		///		 <LI>sTransferType</LI>
		///		 <LI>sRemark. May be SqlString.Null</LI>
		///		 <LI>sRecordStatus</LI>
		///		 <LI>daCreateDate. May be SqlDateTime.Null</LI>
		///		 <LI>sUserCreate. May be SqlString.Null</LI>
		///		 <LI>daUpdateDate. May be SqlDateTime.Null</LI>
		///		 <LI>sUserUpdate. May be SqlString.Null</LI>
		/// </UL>
		/// Properties set after a succesful call of this method: 
		/// <UL>
		///		 <LI>iID</LI>
		/// </UL>
		/// </remarks>
		public override bool Insert()
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[TransferInstruction_Insert]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			scmCmdToExecute.Connection = m_scoMainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sTransferOrderNo", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sTransferOrderNo));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@daInstructionDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daInstructionDate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sGetDataStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sGetDataStatus));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sTransferStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sTransferStatus));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationCode_From", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationCode_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationName_From", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationName_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseCode_From", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseCode_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseName_From", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseName_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseType_From", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseType_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationCode_To", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationCode_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationName_To", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationName_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseCode_To", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseCode_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseName_To", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseName_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseType_To", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseType_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sTransferType", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sTransferType));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sRemark", SqlDbType.NVarChar, 600, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sRemark));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sRecordStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sRecordStatus));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@daCreateDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daCreateDate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sUserCreate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sUserCreate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@daUpdateDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daUpdateDate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sUserUpdate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sUserUpdate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Output, false, 10, 0, "", DataRowVersion.Proposed, m_iID));

				// Open connection.
				m_scoMainConnection.Open();

				// Execute query.
				scmCmdToExecute.ExecuteNonQuery();
				m_iID = (SqlInt32)scmCmdToExecute.Parameters["@iID"].Value;
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("PrTransferInstruction::Insert::Error occured.", ex);
			}
			finally
			{
				// Close connection.
				m_scoMainConnection.Close();
				scmCmdToExecute.Dispose();
			}
		}


		/// <summary>
		/// Purpose: Update method. This method will Update one existing row in the database.
		/// </summary>
		/// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>iID</LI>
		///		 <LI>sTransferOrderNo</LI>
		///		 <LI>daInstructionDate. May be SqlDateTime.Null</LI>
		///		 <LI>sGetDataStatus</LI>
		///		 <LI>sTransferStatus</LI>
		///		 <LI>sLocationCode_From. May be SqlString.Null</LI>
		///		 <LI>sLocationName_From. May be SqlString.Null</LI>
		///		 <LI>sWarehouseCode_From. May be SqlString.Null</LI>
		///		 <LI>sWarehouseName_From. May be SqlString.Null</LI>
		///		 <LI>sWarehouseType_From. May be SqlString.Null</LI>
		///		 <LI>sLocationCode_To. May be SqlString.Null</LI>
		///		 <LI>sLocationName_To. May be SqlString.Null</LI>
		///		 <LI>sWarehouseCode_To. May be SqlString.Null</LI>
		///		 <LI>sWarehouseName_To. May be SqlString.Null</LI>
		///		 <LI>sWarehouseType_To. May be SqlString.Null</LI>
		///		 <LI>sTransferType</LI>
		///		 <LI>sRemark. May be SqlString.Null</LI>
		///		 <LI>sRecordStatus</LI>
		///		 <LI>daCreateDate. May be SqlDateTime.Null</LI>
		///		 <LI>sUserCreate. May be SqlString.Null</LI>
		///		 <LI>daUpdateDate. May be SqlDateTime.Null</LI>
		///		 <LI>sUserUpdate. May be SqlString.Null</LI>
		/// </UL>
		/// </remarks>
		public override bool Update()
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[TransferInstruction_Update]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			scmCmdToExecute.Connection = m_scoMainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sTransferOrderNo", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sTransferOrderNo));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@daInstructionDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daInstructionDate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sGetDataStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sGetDataStatus));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sTransferStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sTransferStatus));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationCode_From", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationCode_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationName_From", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationName_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseCode_From", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseCode_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseName_From", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseName_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseType_From", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseType_From));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationCode_To", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationCode_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sLocationName_To", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sLocationName_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseCode_To", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseCode_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseName_To", SqlDbType.NVarChar, 200, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseName_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sWarehouseType_To", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sWarehouseType_To));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sTransferType", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sTransferType));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sRemark", SqlDbType.NVarChar, 600, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sRemark));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sRecordStatus", SqlDbType.Char, 1, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sRecordStatus));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@daCreateDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daCreateDate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sUserCreate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sUserCreate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@daUpdateDate", SqlDbType.DateTime, 8, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_daUpdateDate));
				scmCmdToExecute.Parameters.Add(new SqlParameter("@sUserUpdate", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, "", DataRowVersion.Proposed, m_sUserUpdate));

				// Open connection.
				m_scoMainConnection.Open();

				// Execute query.
				scmCmdToExecute.ExecuteNonQuery();
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("PrTransferInstruction::Update::Error occured.", ex);
			}
			finally
			{
				// Close connection.
				m_scoMainConnection.Close();
				scmCmdToExecute.Dispose();
			}
		}


		/// <summary>
		/// Purpose: Delete method. This method will Delete one existing row in the database, based on the Primary Key.
		/// </summary>
		/// <returns>True if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>iID</LI>
		/// </UL>
		/// </remarks>
		public override bool Delete()
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[TransferInstruction_Delete]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;

			// Use base class' connection object
			scmCmdToExecute.Connection = m_scoMainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID));

				// Open connection.
				m_scoMainConnection.Open();

				// Execute query.
				scmCmdToExecute.ExecuteNonQuery();
				return true;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("PrTransferInstruction::Delete::Error occured.", ex);
			}
			finally
			{
				// Close connection.
				m_scoMainConnection.Close();
				scmCmdToExecute.Dispose();
			}
		}


		/// <summary>
		/// Purpose: Select method. This method will Select one existing row from the database, based on the Primary Key.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// Properties needed for this method: 
		/// <UL>
		///		 <LI>iID</LI>
		/// </UL>
		///		 <LI>iID</LI>
		///		 <LI>sTransferOrderNo</LI>
		///		 <LI>daInstructionDate</LI>
		///		 <LI>sGetDataStatus</LI>
		///		 <LI>sTransferStatus</LI>
		///		 <LI>sLocationCode_From</LI>
		///		 <LI>sLocationName_From</LI>
		///		 <LI>sWarehouseCode_From</LI>
		///		 <LI>sWarehouseName_From</LI>
		///		 <LI>sWarehouseType_From</LI>
		///		 <LI>sLocationCode_To</LI>
		///		 <LI>sLocationName_To</LI>
		///		 <LI>sWarehouseCode_To</LI>
		///		 <LI>sWarehouseName_To</LI>
		///		 <LI>sWarehouseType_To</LI>
		///		 <LI>sTransferType</LI>
		///		 <LI>sRemark</LI>
		///		 <LI>sRecordStatus</LI>
		///		 <LI>daCreateDate</LI>
		///		 <LI>sUserCreate</LI>
		///		 <LI>daUpdateDate</LI>
		///		 <LI>sUserUpdate</LI>
		/// Will fill all properties corresponding with a field in the table with the value of the row selected.
		/// </remarks>
		public override DataTable SelectOne()
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[TransferInstruction_SelectOne]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable dtToReturn = new DataTable("TransferInstruction");
			SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

			// Use base class' connection object
			scmCmdToExecute.Connection = m_scoMainConnection;

			try
			{
				scmCmdToExecute.Parameters.Add(new SqlParameter("@iID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, "", DataRowVersion.Proposed, m_iID));

				// Open connection.
				m_scoMainConnection.Open();

				// Execute query.
				sdaAdapter.Fill(dtToReturn);
				if(dtToReturn.Rows.Count > 0)
				{
					m_iID = (Int32)dtToReturn.Rows[0]["ID"];
					m_sTransferOrderNo = (string)dtToReturn.Rows[0]["TransferOrderNo"];
					m_daInstructionDate = dtToReturn.Rows[0]["InstructionDate"] == System.DBNull.Value ? SqlDateTime.Null : (DateTime)dtToReturn.Rows[0]["InstructionDate"];
					m_sGetDataStatus = (string)dtToReturn.Rows[0]["GetDataStatus"];
					m_sTransferStatus = (string)dtToReturn.Rows[0]["TransferStatus"];
					m_sLocationCode_From = dtToReturn.Rows[0]["LocationCode_From"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["LocationCode_From"];
					m_sLocationName_From = dtToReturn.Rows[0]["LocationName_From"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["LocationName_From"];
					m_sWarehouseCode_From = dtToReturn.Rows[0]["WarehouseCode_From"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["WarehouseCode_From"];
					m_sWarehouseName_From = dtToReturn.Rows[0]["WarehouseName_From"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["WarehouseName_From"];
					m_sWarehouseType_From = dtToReturn.Rows[0]["WarehouseType_From"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["WarehouseType_From"];
					m_sLocationCode_To = dtToReturn.Rows[0]["LocationCode_To"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["LocationCode_To"];
					m_sLocationName_To = dtToReturn.Rows[0]["LocationName_To"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["LocationName_To"];
					m_sWarehouseCode_To = dtToReturn.Rows[0]["WarehouseCode_To"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["WarehouseCode_To"];
					m_sWarehouseName_To = dtToReturn.Rows[0]["WarehouseName_To"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["WarehouseName_To"];
					m_sWarehouseType_To = dtToReturn.Rows[0]["WarehouseType_To"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["WarehouseType_To"];
					m_sTransferType = (string)dtToReturn.Rows[0]["TransferType"];
					m_sRemark = dtToReturn.Rows[0]["Remark"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["Remark"];
					m_sRecordStatus = (string)dtToReturn.Rows[0]["RecordStatus"];
					m_daCreateDate = dtToReturn.Rows[0]["CreateDate"] == System.DBNull.Value ? SqlDateTime.Null : (DateTime)dtToReturn.Rows[0]["CreateDate"];
					m_sUserCreate = dtToReturn.Rows[0]["UserCreate"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["UserCreate"];
					m_daUpdateDate = dtToReturn.Rows[0]["UpdateDate"] == System.DBNull.Value ? SqlDateTime.Null : (DateTime)dtToReturn.Rows[0]["UpdateDate"];
					m_sUserUpdate = dtToReturn.Rows[0]["UserUpdate"] == System.DBNull.Value ? SqlString.Null : (string)dtToReturn.Rows[0]["UserUpdate"];
				}
				return dtToReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("PrTransferInstruction::SelectOne::Error occured.", ex);
			}
			finally
			{
				// Close connection.
				m_scoMainConnection.Close();
				scmCmdToExecute.Dispose();
				sdaAdapter.Dispose();
			}
		}


		/// <summary>
		/// Purpose: SelectAll method. This method will Select all rows from the table.
		/// </summary>
		/// <returns>DataTable object if succeeded, otherwise an Exception is thrown. </returns>
		/// <remarks>
		/// </remarks>
		public override DataTable SelectAll()
		{
			SqlCommand	scmCmdToExecute = new SqlCommand();
			scmCmdToExecute.CommandText = "dbo.[TransferInstruction_SelectAll]";
			scmCmdToExecute.CommandType = CommandType.StoredProcedure;
			DataTable dtToReturn = new DataTable("TransferInstruction");
			SqlDataAdapter sdaAdapter = new SqlDataAdapter(scmCmdToExecute);

			// Use base class' connection object
			scmCmdToExecute.Connection = m_scoMainConnection;

			try
			{

				// Open connection.
				m_scoMainConnection.Open();

				// Execute query.
				sdaAdapter.Fill(dtToReturn);
				return dtToReturn;
			}
			catch(Exception ex)
			{
				// some error occured. Bubble it to caller and encapsulate Exception object
				throw new Exception("PrTransferInstruction::SelectAll::Error occured.", ex);
			}
			finally
			{
				// Close connection.
				m_scoMainConnection.Close();
				scmCmdToExecute.Dispose();
				sdaAdapter.Dispose();
			}
		}


		#region Class Property Declarations
		public SqlInt32 iID
		{
			get
			{
				return m_iID;
			}
			set
			{
				SqlInt32 iIDTmp = (SqlInt32)value;
				if(iIDTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("iID", "iID can't be NULL");
				}
				m_iID = value;
			}
		}


		public SqlString sTransferOrderNo
		{
			get
			{
				return m_sTransferOrderNo;
			}
			set
			{
				SqlString sTransferOrderNoTmp = (SqlString)value;
				if(sTransferOrderNoTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sTransferOrderNo", "sTransferOrderNo can't be NULL");
				}
				m_sTransferOrderNo = value;
			}
		}


		public SqlDateTime daInstructionDate
		{
			get
			{
				return m_daInstructionDate;
			}
			set
			{
				SqlDateTime daInstructionDateTmp = (SqlDateTime)value;
				if(daInstructionDateTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("daInstructionDate", "daInstructionDate can't be NULL");
				}
				m_daInstructionDate = value;
			}
		}


		public SqlString sGetDataStatus
		{
			get
			{
				return m_sGetDataStatus;
			}
			set
			{
				m_sGetDataStatus = value;
			}
		}


		public SqlString sTransferStatus
		{
			get
			{
				return m_sTransferStatus;
			}
			set
			{
				m_sTransferStatus = value;
			}
		}


		public SqlString sLocationCode_From
		{
			get
			{
				return m_sLocationCode_From;
			}
			set
			{
				SqlString sLocationCode_FromTmp = (SqlString)value;
				if(sLocationCode_FromTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sLocationCode_From", "sLocationCode_From can't be NULL");
				}
				m_sLocationCode_From = value;
			}
		}


		public SqlString sLocationName_From
		{
			get
			{
				return m_sLocationName_From;
			}
			set
			{
				SqlString sLocationName_FromTmp = (SqlString)value;
				if(sLocationName_FromTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sLocationName_From", "sLocationName_From can't be NULL");
				}
				m_sLocationName_From = value;
			}
		}


		public SqlString sWarehouseCode_From
		{
			get
			{
				return m_sWarehouseCode_From;
			}
			set
			{
				SqlString sWarehouseCode_FromTmp = (SqlString)value;
				if(sWarehouseCode_FromTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sWarehouseCode_From", "sWarehouseCode_From can't be NULL");
				}
				m_sWarehouseCode_From = value;
			}
		}


		public SqlString sWarehouseName_From
		{
			get
			{
				return m_sWarehouseName_From;
			}
			set
			{
				SqlString sWarehouseName_FromTmp = (SqlString)value;
				if(sWarehouseName_FromTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sWarehouseName_From", "sWarehouseName_From can't be NULL");
				}
				m_sWarehouseName_From = value;
			}
		}


		public SqlString sWarehouseType_From
		{
			get
			{
				return m_sWarehouseType_From;
			}
			set
			{
				SqlString sWarehouseType_FromTmp = (SqlString)value;
				if(sWarehouseType_FromTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sWarehouseType_From", "sWarehouseType_From can't be NULL");
				}
				m_sWarehouseType_From = value;
			}
		}


		public SqlString sLocationCode_To
		{
			get
			{
				return m_sLocationCode_To;
			}
			set
			{
				SqlString sLocationCode_ToTmp = (SqlString)value;
				if(sLocationCode_ToTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sLocationCode_To", "sLocationCode_To can't be NULL");
				}
				m_sLocationCode_To = value;
			}
		}


		public SqlString sLocationName_To
		{
			get
			{
				return m_sLocationName_To;
			}
			set
			{
				SqlString sLocationName_ToTmp = (SqlString)value;
				if(sLocationName_ToTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sLocationName_To", "sLocationName_To can't be NULL");
				}
				m_sLocationName_To = value;
			}
		}


		public SqlString sWarehouseCode_To
		{
			get
			{
				return m_sWarehouseCode_To;
			}
			set
			{
				SqlString sWarehouseCode_ToTmp = (SqlString)value;
				if(sWarehouseCode_ToTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sWarehouseCode_To", "sWarehouseCode_To can't be NULL");
				}
				m_sWarehouseCode_To = value;
			}
		}


		public SqlString sWarehouseName_To
		{
			get
			{
				return m_sWarehouseName_To;
			}
			set
			{
				SqlString sWarehouseName_ToTmp = (SqlString)value;
				if(sWarehouseName_ToTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sWarehouseName_To", "sWarehouseName_To can't be NULL");
				}
				m_sWarehouseName_To = value;
			}
		}


		public SqlString sWarehouseType_To
		{
			get
			{
				return m_sWarehouseType_To;
			}
			set
			{
				SqlString sWarehouseType_ToTmp = (SqlString)value;
				if(sWarehouseType_ToTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sWarehouseType_To", "sWarehouseType_To can't be NULL");
				}
				m_sWarehouseType_To = value;
			}
		}


		public SqlString sTransferType
		{
			get
			{
				return m_sTransferType;
			}
			set
			{
				SqlString sTransferTypeTmp = (SqlString)value;
				if(sTransferTypeTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sTransferType", "sTransferType can't be NULL");
				}
				m_sTransferType = value;
			}
		}


		public SqlString sRemark
		{
			get
			{
				return m_sRemark;
			}
			set
			{
				SqlString sRemarkTmp = (SqlString)value;
				if(sRemarkTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sRemark", "sRemark can't be NULL");
				}
				m_sRemark = value;
			}
		}


		public SqlString sRecordStatus
		{
			get
			{
				return m_sRecordStatus;
			}
			set
			{
				m_sRecordStatus = value;
			}
		}


		public SqlDateTime daCreateDate
		{
			get
			{
				return m_daCreateDate;
			}
			set
			{
				SqlDateTime daCreateDateTmp = (SqlDateTime)value;
				if(daCreateDateTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("daCreateDate", "daCreateDate can't be NULL");
				}
				m_daCreateDate = value;
			}
		}


		public SqlString sUserCreate
		{
			get
			{
				return m_sUserCreate;
			}
			set
			{
				SqlString sUserCreateTmp = (SqlString)value;
				if(sUserCreateTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sUserCreate", "sUserCreate can't be NULL");
				}
				m_sUserCreate = value;
			}
		}


		public SqlDateTime daUpdateDate
		{
			get
			{
				return m_daUpdateDate;
			}
			set
			{
				SqlDateTime daUpdateDateTmp = (SqlDateTime)value;
				if(daUpdateDateTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("daUpdateDate", "daUpdateDate can't be NULL");
				}
				m_daUpdateDate = value;
			}
		}


		public SqlString sUserUpdate
		{
			get
			{
				return m_sUserUpdate;
			}
			set
			{
				SqlString sUserUpdateTmp = (SqlString)value;
				if(sUserUpdateTmp.IsNull)
				{
					throw new ArgumentOutOfRangeException("sUserUpdate", "sUserUpdate can't be NULL");
				}
				m_sUserUpdate = value;
			}
		}
		#endregion
	}
}
