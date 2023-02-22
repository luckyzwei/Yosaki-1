using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class MonthlyPassAttendAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/06.Table/MonthlyPassAttend.xlsx";
    private static readonly string assetFilePath = "Assets/06.Table/MonthlyPassAttend.asset";
    private static readonly string sheetName = "MonthlyPassAttend";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            MonthlyPassAttend data = (MonthlyPassAttend)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(MonthlyPassAttend));
            if (data == null) {
                data = ScriptableObject.CreateInstance<MonthlyPassAttend> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<MonthlyPassAttendData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<MonthlyPassAttendData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
