﻿
/*
	File Generated by NetTiers templates [www.nettiers.com]
	Generated on : July 10, 2013
	Important: Do not modify this file. Edit the file MediaAudioTest.cs instead.
*/

#region Using directives

using System;
using System.Xml;
using System.Xml.Serialization;
using NUnit.Framework;
using ShutterTale.Entities;
using ShutterTale.Data;
using ShutterTale.Data.Bases;

#endregion

namespace ShutterTale.UnitTests
{
    /// <summary>
    /// Provides tests for the and <see cref="MediaAudio"/> objects (entity, collection and repository).
    /// </summary>
   public partial class MediaAudioTest
    {
    	// the MediaAudio instance used to test the repository.
		protected MediaAudio mock;
		
		// the TList<MediaAudio> instance used to test the repository.
		protected TList<MediaAudio> mockCollection;
		
		protected static TransactionManager CreateTransaction()
		{
			TransactionManager transactionManager = null;
			if (DataRepository.Provider.IsTransactionSupported)
			{
				transactionManager = DataRepository.Provider.CreateTransaction();
				transactionManager.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);
			}			
			return transactionManager;
		}
		       
        /// <summary>
		/// This method is used to construct the test environment prior to running the tests.
		/// </summary>        
        static public void Init_Generated()
        {		
        	System.Console.WriteLine(new String('-', 75));
			System.Console.WriteLine("-- Testing the MediaAudio Entity with the {0} --", ShutterTale.Data.DataRepository.Provider.Name);
			System.Console.WriteLine(new String('-', 75));
        }
    
    	/// <summary>
		/// This method is used to restore the environment after the tests are completed.
		/// </summary>
		static public void CleanUp_Generated()
        {   		
			System.Console.WriteLine("All Tests Completed");
			System.Console.WriteLine();
        }
    
    
		/// <summary>
		/// Inserts a mock MediaAudio entity into the database.
		/// </summary>
		private void Step_01_Insert_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock = CreateMockInstance(tm);
				Assert.IsTrue(DataRepository.MediaAudioProvider.Insert(tm, mock), "Insert failed");
										
				System.Console.WriteLine("DataRepository.MediaAudioProvider.Insert(mock):");			
				System.Console.WriteLine(mock);			
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		
		/// <summary>
		/// Selects all MediaAudio objects of the database.
		/// </summary>
		private void Step_02_SelectAll_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				//Find
				int count = -1;
				
				mockCollection = DataRepository.MediaAudioProvider.Find(tm, null, "", 0, 10, out count );
				Assert.IsTrue(count >= 0 && mockCollection != null, "Query Failed to issue Find Command.");
				
				System.Console.WriteLine("DataRepository.MediaAudioProvider.Find():");			
				System.Console.WriteLine(mockCollection);
				
				// GetPaged
				count = -1;
				
				mockCollection = DataRepository.MediaAudioProvider.GetPaged(tm, 0, 10, out count);
				Assert.IsTrue(count >= 0 && mockCollection != null, "Query Failed to issue GetPaged Command.");
				System.Console.WriteLine("#get paged count: " + count.ToString());
			}
		}
		
		
		
		
		/// <summary>
		/// Deep load all MediaAudio children.
		/// </summary>
		private void Step_03_DeepLoad_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				int count = -1;
				mock =  CreateMockInstance(tm);
				mockCollection = DataRepository.MediaAudioProvider.GetPaged(tm, 0, 10, out count);
			
				DataRepository.MediaAudioProvider.DeepLoading += new EntityProviderBaseCore<MediaAudio, MediaAudioKey>.DeepLoadingEventHandler(
						delegate(object sender, DeepSessionEventArgs e)
						{
							if (e.DeepSession.Count > 3)
								e.Cancel = true;
						}
					);

				if (mockCollection.Count > 0)
				{
					
					DataRepository.MediaAudioProvider.DeepLoad(tm, mockCollection[0]);
					System.Console.WriteLine("MediaAudio instance correctly deep loaded at 1 level.");
									
					mockCollection.Add(mock);
					// DataRepository.MediaAudioProvider.DeepSave(tm, mockCollection);
				}
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		/// <summary>
		/// Updates a mock MediaAudio entity into the database.
		/// </summary>
		private void Step_04_Update_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				MediaAudio mock = CreateMockInstance(tm);
				Assert.IsTrue(DataRepository.MediaAudioProvider.Insert(tm, mock), "Insert failed");
				
				UpdateMockInstance(tm, mock);
				Assert.IsTrue(DataRepository.MediaAudioProvider.Update(tm, mock), "Update failed.");			
				
				System.Console.WriteLine("DataRepository.MediaAudioProvider.Update(mock):");			
				System.Console.WriteLine(mock);
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		
		/// <summary>
		/// Delete the mock MediaAudio entity into the database.
		/// </summary>
		private void Step_05_Delete_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock =  (MediaAudio)CreateMockInstance(tm);
				DataRepository.MediaAudioProvider.Insert(tm, mock);
			
				Assert.IsTrue(DataRepository.MediaAudioProvider.Delete(tm, mock), "Delete failed.");
				System.Console.WriteLine("DataRepository.MediaAudioProvider.Delete(mock):");			
				System.Console.WriteLine(mock);
				
				//normally one would commit here
				//tm.Commit();
				//IDisposable will Rollback Transaction since it's left uncommitted
			}
		}
		
		#region Serialization tests
		
		/// <summary>
		/// Serialize the mock MediaAudio entity into a temporary file.
		/// </summary>
		private void Step_06_SerializeEntity_Generated()
		{	
			using (TransactionManager tm = CreateTransaction())
			{
				mock =  CreateMockInstance(tm);
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_MediaAudio.xml");
			
				EntityHelper.SerializeXml(mock, fileName);
				Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock not found");
					
				System.Console.WriteLine("mock correctly serialized to a temporary file.");			
			}
		}
		
		/// <summary>
		/// Deserialize the mock MediaAudio entity from a temporary file.
		/// </summary>
		private void Step_07_DeserializeEntity_Generated()
		{
			string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_MediaAudio.xml");
			Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock file not found to deserialize");
			
			using (System.IO.StreamReader sr = System.IO.File.OpenText(fileName))
			{
				object item = EntityHelper.DeserializeEntityXml<MediaAudio>(sr.ReadToEnd());
				sr.Close();
			}
			System.IO.File.Delete(fileName);
			
			System.Console.WriteLine("mock correctly deserialized from a temporary file.");
		}
		
		/// <summary>
		/// Serialize a MediaAudio collection into a temporary file.
		/// </summary>
		private void Step_08_SerializeCollection_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_MediaAudioCollection.xml");
				
				mock = CreateMockInstance(tm);
				TList<MediaAudio> mockCollection = new TList<MediaAudio>();
				mockCollection.Add(mock);
			
				EntityHelper.SerializeXml(mockCollection, fileName);
				
				Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock collection not found");
				System.Console.WriteLine("TList<MediaAudio> correctly serialized to a temporary file.");					
			}
		}
		
		
		/// <summary>
		/// Deserialize a MediaAudio collection from a temporary file.
		/// </summary>
		private void Step_09_DeserializeCollection_Generated()
		{
			string fileName = System.IO.Path.Combine(System.IO.Path.GetTempPath(), "temp_MediaAudioCollection.xml");
			Assert.IsTrue(System.IO.File.Exists(fileName), "Serialized mock file not found to deserialize");
			
			XmlSerializer mySerializer = new XmlSerializer(typeof(TList<MediaAudio>)); 
			using (System.IO.FileStream myFileStream = new System.IO.FileStream(fileName,  System.IO.FileMode.Open))
			{
				TList<MediaAudio> mockCollection = (TList<MediaAudio>) mySerializer.Deserialize(myFileStream);
				myFileStream.Close();
			}
			
			System.IO.File.Delete(fileName);
			System.Console.WriteLine("TList<MediaAudio> correctly deserialized from a temporary file.");	
		}
		#endregion
		
		
		
		/// <summary>
		/// Check the foreign key dal methods.
		/// </summary>
		private void Step_10_FK_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				MediaAudio entity = CreateMockInstance(tm);
				bool result = DataRepository.MediaAudioProvider.Insert(tm, entity);
				
				Assert.IsTrue(result, "Could Not Test FK, Insert Failed");
				
			}
		}
		
		
		/// <summary>
		/// Check the indexes dal methods.
		/// </summary>
		private void Step_11_IX_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				MediaAudio entity = CreateMockInstance(tm);
				bool result = DataRepository.MediaAudioProvider.Insert(tm, entity);
				
				Assert.IsTrue(result, "Could Not Test IX, Insert Failed");

			
				MediaAudio t0 = DataRepository.MediaAudioProvider.GetById(tm, entity.Id);
			}
		}
		
		/// <summary>
		/// Test methods exposed by the EntityHelper class.
		/// </summary>
		private void Step_20_TestEntityHelper_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				mock = CreateMockInstance(tm);
				
				MediaAudio entity = mock.Copy() as MediaAudio;
				entity = (MediaAudio)mock.Clone();
				Assert.IsTrue(MediaAudio.ValueEquals(entity, mock), "Clone is not working");
			}
		}
		
		/// <summary>
		/// Test Find using the Query class
		/// </summary>
		private void Step_30_TestFindByQuery_Generated()
		{
			using (TransactionManager tm = CreateTransaction())
			{
				//Insert Mock Instance
				MediaAudio mock = CreateMockInstance(tm);
				bool result = DataRepository.MediaAudioProvider.Insert(tm, mock);
				
				Assert.IsTrue(result, "Could Not Test FindByQuery, Insert Failed");

				MediaAudioQuery query = new MediaAudioQuery();
			
				query.AppendEquals(MediaAudioColumn.AudioChannels, mock.AudioChannels.ToString());
				query.AppendEquals(MediaAudioColumn.Duration, mock.Duration.ToString());
				query.AppendEquals(MediaAudioColumn.AudioCodec, mock.AudioCodec.ToString());
				query.AppendEquals(MediaAudioColumn.Id, mock.Id.ToString());
				
				TList<MediaAudio> results = DataRepository.MediaAudioProvider.Find(tm, query);
				
				Assert.IsTrue(results.Count == 1, "Find is not working correctly.  Failed to find the mock instance");
			}
		}
						
		#region Mock Instance
		///<summary>
		///  Returns a Typed MediaAudio Entity with mock values.
		///</summary>
		static public MediaAudio CreateMockInstance_Generated(TransactionManager tm)
		{		
			MediaAudio mock = new MediaAudio();
						
			mock.AudioChannels = TestUtility.Instance.RandomByte();
			mock.Duration = TestUtility.Instance.RandomString(2, false);;
			mock.AudioCodec = TestUtility.Instance.RandomString(2, false);;
			
			//OneToOneRelationship
			Media mockMediaById = MediaTest.CreateMockInstance(tm);
			DataRepository.MediaProvider.Insert(tm, mockMediaById);
			mock.Id = mockMediaById.Id;
		
			// create a temporary collection and add the item to it
			TList<MediaAudio> tempMockCollection = new TList<MediaAudio>();
			tempMockCollection.Add(mock);
			tempMockCollection.Remove(mock);
			
		
		   return (MediaAudio)mock;
		}
		
		
		///<summary>
		///  Update the Typed MediaAudio Entity with modified mock values.
		///</summary>
		static public void UpdateMockInstance_Generated(TransactionManager tm, MediaAudio mock)
		{
			mock.AudioChannels = TestUtility.Instance.RandomByte();
			mock.Duration = TestUtility.Instance.RandomString(2, false);;
			mock.AudioCodec = TestUtility.Instance.RandomString(2, false);;
			
			//OneToOneRelationship
			Media mockMediaById = MediaTest.CreateMockInstance(tm);
			DataRepository.MediaProvider.Insert(tm, mockMediaById);
			mock.Id = mockMediaById.Id;
					
		}
		#endregion
    }
}
