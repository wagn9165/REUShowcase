<?php include('server.php')?>

<!------------- HTML ------------->
<!DOCTYPE html>
<head>
<title>Doctor Portal Charges</title>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue.css">
  <link href="css/doctorPortal.css" rel='stylesheet'>
  <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
</head>

<body>
  <!--Navbar-->
  <div class="w3-bar w3-theme w3-large">
	<a href="DoctorPortalIndex.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">HOME</a>
	<a href="DoctorPortalDiagnosis.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Diagnosis</a>
	<a href="DoctorPortalPrescriptions.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Prescriptions</a>
	<a href="DoctorPortalServices.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Services</a>
	<a href="DoctorPortalCharges.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Charges</a>
	<a class="w3-bar-item w3-hide-small w3-white">Reports</a>
    <div class="nav-right">
      <a href="DoctorPortalIndex.php?logout='1'" class="w3-bar-item w3-button w3-hide-small w3-hover-white">LOGOUT</a>
    </div>
  </div>

  <!--Header-->
  <div class="header w3-theme-d4">
    <h1><b>Reports</b></h1>
  </div>

  <!--Form Errors-->
  <div class="error center">
	<b><?php include('errors.php'); ?></b>
  </div>
	
  <!--View Charges Form-->
  <form id="chargesForm" class="center" method="post" action="<?php echo htmlspecialchars($_SERVER['PHP_SELF']);?>">

	<?php
		$cat=$_GET['cat'];
		$operSaved=$_GET['oper'];
		$yearSaved=$_GET['year'];
		//Year Selection
		echo "<div class=\"input-group\">";
		echo "<label><b>Year</b></label>";
		echo "<select id=\"year\" name = \"Year\">";
			$query2 = "SELECT DISTINCT YEAR(CharDate) FROM Charges ORDER BY YEAR(CharDate);";
			$result2 = mysqli_query($db2, $query2);
			
			if("All"==$yearSaved)
			{
				echo "<option value=\"All\" selected>All Years</option>";
			}
			else
			{
				echo "<option value=\"All\">All Years</option>";
			}
			
			//Add to option list
			while($row = mysqli_fetch_assoc($result2))
			{
				//echo "<option>$row</option>";
				$prod_name = $row['YEAR(CharDate)'];
				if($prod_name==$yearSaved)
				{
					echo "<option value='$prod_name' selected>$prod_name</option>";
				}
				else
				{
					echo "<option value='$prod_name'>$prod_name</option>";
				}
				
			}
		echo "</select>";
		echo "</div>";
		//print_r($result);
		
		//Operator Selection
		echo "<div class=\"input-group\">";
		echo "<label><b>Operator</b></label>";
		echo "<select id=\"oper\" name = \"Operator\">";
			if($operSaved)
			{
				if($operSaved == "SUM")
				{
					echo "<option>Select</option>";
					echo "<option value=\"SUM\" selected>Show Total</option>";
					echo "<option value=\"RAW\">Show Entries</option>";
					echo "<option value=\"COUNT\">Show Number of Occurences</option>";
				}
				else if($operSaved == "RAW")
				{
					echo "<option>Select</option>";
					echo "<option value=\"SUM\">Show Total</option>";
					echo "<option value=\"RAW\" selected>Show Entries</option>";
					echo "<option value=\"COUNT\">Show Number of Occurences</option>";
				}
				else if($operSaved == "COUNT")
				{
					echo "<option>Select</option>";
					echo "<option value=\"SUM\">Show Total</option>";
					echo "<option value=\"RAW\">Show Entries</option>";
					echo "<option value=\"COUNT\" selected>Show Number of Occurences</option>";
				}
				else
				{
					echo "<option>Select</option>";
					echo "<option value=\"SUM\">Show Total</option>";
					echo "<option value=\"RAW\">Show Entries</option>";
					echo "<option value=\"COUNT\">Show Number of Occurences</option>";
				}
			}
			else
			{
				echo "<option>Select</option>";
				echo "<option value=\"SUM\">Show Total</option>";
				echo "<option value=\"RAW\">Show Entries</option>";
				echo "<option value=\"COUNT\">Show Number of Occurences</option>";
			}
		echo "</select>";
		echo "</div>";
		
		
		//<!--Main Query Selection-->
		echo "<div class=\"input-group\">";
		echo "<label><b>Queries</b></label>";
		echo "<select id=\"q1\" onChange='reload()' name = \"Queries\">";
			//Manually checking because there are only three values
			if($cat)
			{
				if($cat == "default")
				{
					echo "<option>Select</option>";
					echo "<option value=\"default\" selected>Show All Charges</option>";
					echo "<option value=\"MedicalProducts\">Charges by Prescription</option>";
					echo "<option value=\"ServicesOffered\">Charges by Service</option>";
				}
				else if($cat == "MedicalProducts")
				{
					echo "<option>Select</option>";
					echo "<option value=\"default\">Show All Charges</option>";
					echo "<option value=\"MedicalProducts\" selected>Charges by Prescription</option>";
					echo "<option value=\"ServicesOffered\">Charges by Service</option>";
				}
				else if($cat == "ServicesOffered")
				{
					echo "<option>Select</option>";
					echo "<option value=\"default\">Show All Charges</option>";
					echo "<option value=\"MedicalProducts\">Charges by Prescription</option>";
					echo "<option value=\"ServicesOffered\" selected>Charges by Service</option>";
				}
				else
				{
					echo "<option>Select</option>";
					echo "<option value=\"default\">Show All Charges</option>";
					echo "<option value=\"MedicalProducts\">Charges by Prescription</option>";
					echo "<option value=\"ServicesOffered\">Charges by Service</option>";
				}
			}
			else
			{	
				echo "<option>Select</option>";
				echo "<option value=\"default\">Show All Charges</option>";
				echo "<option value=\"MedicalProducts\">Charges by Prescription</option>";
				echo "<option value=\"ServicesOffered\">Charges by Service</option>";
			}
		echo "</select>";
		echo "</div>";
		
		//Sub Query Selection
			
		//echo $cat;
		
		//Category == MedicalProducts
		if($cat == "MedicalProducts")
		{	
			echo "<div class=\"input-group\">";
			echo "<label><b>Subqueries</b></label>";
			echo "<select id=\"q2\" name=\"SubQuery\">";
			echo "<option>Select</option>";
				  
			//Query for Prescription Name
			$query2 = "SELECT Name FROM ".$cat.";";
			$result = mysqli_query($db2, $query2);
			
			//Add to option list
			while($row = mysqli_fetch_assoc($result))
			{
				$prod_name = $row['Name'];
				echo "<option value='$prod_name'>$prod_name</option>";
			}
			echo "</select>";
			echo "</div>";
		}
		//Category == ServicesOffered
		else if($cat == "ServicesOffered")
		{
			echo "<div class=\"input-group\">";
			echo "<label><b>Subqueries</b></label>";
			echo "<select id=\"q2\" name=\"SubQuery\">";
			echo "<option>Select</option>";
				  
			//Query for Service Name
			$query2 = "SELECT Description FROM ".$cat.";";
			$result = mysqli_query($db2, $query2);
			
			//Add to option list
			while($row = mysqli_fetch_assoc($result))
			  {
				$prod_name = $row['Description'];
				echo "<option value='$prod_name'>$prod_name</option>";
			  }
			/*if($stmt = $db2->prepare($query2))
			{
			  echo "preparing";
			  $stmt->bind_param('s',$cat);
			  echo "<option value='$stmt'>$stmt</option>";
			  $stmt->execute();
			  $r_set=$stmt->get_result();	

			  
			}
			else
			{
			  echo "<option>Error prepare</option>";
			}*/
			echo "</select>";
			echo "</div>";
		}
		else
		{
			//echo "cat == \"default\"";
		}
		  
	?>
	  

	<!--Submit Button-->
	<div class="input-group">
	  <button type="submit" class="w3-button w3-round w3-blue w3-ripple" name="select_reports">View All Charges</button>
	</div>
  </form>

  <!--Charges Display-->
  <div class="home-middle">
    <?php
	  if ($result != null) {
		//echo "result was  not null";
		while($assoc = mysqli_fetch_assoc($result))
		{
			echo "<div style='border: 1px solid black'>";
			echo "<pre>";
			print_r($assoc);
			echo "</pre>";
			echo "</div>";
			echo nl2br("\n");

		}
	  }
	  else
	  {
		  
	  }
		
	?>
  </div>
<script>
function reload() {
	var v1=document.getElementById('q1').value;
	var v2=document.getElementById('oper').value;
	var v3=document.getElementById('year').value;
	//document.write(v1);
	self.location='DoctorPortalReports.php?cat=' + v1 + '&oper=' + v2 + '&year=' + v3;
}
</script>
</body>
</html>