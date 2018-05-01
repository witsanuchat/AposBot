<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Notify.aspx.cs" Inherits="Line_Bot_Application2.Notify" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ThaiCreate.Com PHP & SQL Server Tutorial</title>
</head>
<body>
<?php  
          
        $connectionTimeoutSeconds = 30; 
        $maxCountTriesConnectAndQuery = 3;  
        $secondsBetweenRetries = 4;  
        $errNo = 0;  
        $serverName = "tcp:yourdatabase.database.windows.net,1433";  
        $connectionOptions = array("Database"=>"AdventureWorks", "Uid"=>"yourusername", "PWD"=>"yourpassword", "LoginTimeout" => $connectionTimeoutSeconds);  
           
        $conn;  
        $errorArr = array();  
        for ($cc = 1; $cc <= $maxCountTriesConnectAndQuery; $cc++)  
        {  
            $errorArr = array();  
            $ctr = 0;  
           
            $conn = sqlsrv_connect($serverName, $connectionOptions);    
            if( $conn == true)  
            {  
                echo "Connection was established";  
                echo "<br>";  

                $tsql = "SELECT [CompanyName] FROM SalesLT.Customer";  
                $getProducts = sqlsrv_query($conn, $tsql);  
                if ($getProducts == FALSE)  
                    die(FormatErrors(sqlsrv_errors()));  
                $productCount = 0;  
                $ctr = 0;  
                while($row = sqlsrv_fetch_array($getProducts, SQLSRV_FETCH_ASSOC))  
                {     
                    $ctr++;  
                    echo($row['CompanyName']);  
                    echo("<br/>");  
                    $productCount++;  
                    if($ctr>10)  
                        break;  
                }  
                sqlsrv_free_stmt($getProducts);  
                break;  
            }  
            // Adds any the error codes from the SQL Exception to an array.  
            else {    
                if( ($errors = sqlsrv_errors() ) != null) {  
                    foreach( $errors as $error ) {  
                        $errorArr[$ctr] = $error['code'];  
                        $ctr = $ctr + 1;  
                    }  
                }  
                $isTransientError = TRUE;  
                
                $isTransientError = IsTransientStatic($errorArr);  
                if ($isTransientError == TRUE)  // Is a static persistent error...  
                {  
                    echo("Persistent error suffered, SqlException.Number==". $errorArr[0].". Program Will terminate.");  
                    echo "<br>";  
                   
                    exit(0);  
                }  
               
                if ($cc >= $maxCountTriesConnectAndQuery)  
                {  
                    echo "Transient errors suffered in too many retries - " . $cc ." Program will terminate.";  
                    echo "<br>";  
                    exit(0);  
                }  
                echo("Transient error encountered.  SqlException.Number==". $errorArr[0]. " . Program might retry by itself.");    
                echo "<br>";  
                echo $cc . " Attempts so far. Might retry.";  
                echo "<br>";  
             
                sleep(1*$secondsBetweenRetries);  
            }  
           
        }  
        function IsTransientStatic($errorArr) {  
            $arrayOfTransientErrorNumbers = array(4060, 10928, 10929, 40197, 40501, 40613);  
            $result = array_intersect($arrayOfTransientErrorNumber, $errorArr);  
            $count = count($result);  
            if($count >= 0) //change to > 0 later.  
                return TRUE;  
        }  
    ?>
<table width="600" border="1">
  <tr>
    <th width="91"> <div align="center">CustomerID </div></th>
    <th width="98"> <div align="center">Name </div></th>
    <th width="198"> <div align="center">Email </div></th>
    <th width="97"> <div align="center">CountryCode </div></th>
    <th width="59"> <div align="center">Budget </div></th>
    <th width="71"> <div align="center">Used </div></th>
  </tr>
<?php
while($objResult = mssql_fetch_array($objQuery))
{
?>
  <tr>
    <td><div align="center"><?php echo $objResult["CustomerID"];?></div></td>
    <td><?php echo $objResult["Name"];?></td>
    <td><?php echo $objResult["Email"];?></td>
    <td><div align="center"><?php echo $objResult["CountryCode"];?></div></td>
    <td align="right"><?php echo $objResult["Budget"];?></td>
    <td align="right"><?php echo $objResult["Used"];?></td>
  </tr>
<?php
}
?>
</table>
<?php
mssql_close($objConnect);
?>
</body>
</html>