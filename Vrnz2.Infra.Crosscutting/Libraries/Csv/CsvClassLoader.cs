using System;
using System.Reflection;

namespace Vrnz2.Infra.CrossCutting.Libraries.Csv
{
    public class CsvClassLoader
    {
        public static X LoadNewCsv<X>(string[] fields, bool supressErrors)
        {
            X tempObj = (X)Activator.CreateInstance(typeof(X));

            LoadCsv(tempObj, fields, supressErrors);

            return tempObj;
        }

        public static void LoadCsv(object target, string[] fields, bool supressErrors)
        {
            Type targetType = target.GetType();
            PropertyInfo[] properties = targetType.GetProperties();

            // Loop through properties
            foreach (PropertyInfo property in properties)
            {
                // Make sure the property is writeable (has a Set operation)
                if (property.CanWrite)
                {
                    // find CSVPosition attributes assigned to the current property
                    object[] attributes = property.GetCustomAttributes(typeof(CsvLineIndexAttribute), false);

                    // if Length is greater than 0 we have at least one CSVPositionAttribute
                    if (attributes.Length > 0)
                    {
                        // We will only process the first CSVPositionAttribute
                        CsvLineIndexAttribute positionAttr = (CsvLineIndexAttribute)attributes[0];

                        //Retrieve the postion value from the CSVPositionAttribute
                        int position = positionAttr.Position;

                        try
                        {
                            // get the CSV data to be manipulate and written to object
                            object data = fields[position];

                            // check for a Tranform operation that needs to be executed
                            if (positionAttr.DataTransform != string.Empty)
                            {
                                // Get a MethodInfo object pointing to the method declared by the
                                // DataTransform property on our CSVPosition attribute
                                MethodInfo method = targetType.GetMethod(positionAttr.DataTransform);

                                // Invoke the DataTransform method and get the newly formated data
                                data = method.Invoke(target, new object[] { data });
                            }
                            // set the ue on our target object with the data
                            property.SetValue(target, Convert.ChangeType(data, property.PropertyType), null);
                        }
                        catch
                        {
                            // simple error handling
                            if (!supressErrors)
                                throw;
                        }

                    }
                }
            }
        }
    }
}
