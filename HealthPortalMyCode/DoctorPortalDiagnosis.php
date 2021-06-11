<?php include('server.php') ?>

<!------------- HTML ------------->
<!DOCTYPE html>
<head>
  <title>Doctor Portal Diagnosis</title>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue.css">
  <link href="css/doctorPortal.css" rel='stylesheet'>
  <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
</head>

<body>
  <!--Navbar-->
  <div class="w3-bar w3-theme w3-large">
	<a href="DoctorPortalIndex.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">HOME</a>
	<a class="w3-bar-item w3-hide-small w3-white">Diagnosis</a>
	<a href="DoctorPortalPrescriptions.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Prescriptions</a>
	<a href="DoctorPortalServices.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Services</a>
	<a href="DoctorPortalCharges.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Charges</a>
	<a href="DoctorPortalReports.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Reports</a>
    <div class="nav-right">
      <a href="DoctorPortalIndex.php?logout='1'" class="w3-bar-item w3-button w3-hide-small w3-hover-white">LOGOUT</a>
    </div>
  </div>

  <!--Header-->
  <div class="header w3-theme-d4">
    <h1><b>Diagnosis</b></h1>
  </div>

  <!--Form Errors-->
  <div class="error center">
	<b><?php include('errors.php'); ?></b>
  </div>

  <!--Add Diagnosis Form-->
    <form id="diagnosisForm" class="center" method="post" action="<?php echo htmlspecialchars($_SERVER['PHP_SELF']);?>">
	  <h2>Add Diagnosis</h2>

	  <!--PID Input-->
	  <div class="input-group">
	  	<label><b>PID</b></label>
		<select name = "pid" class="form-control"> <br>
			<option>Select</option>;
			<?php			
			$did = $_SESSION['did'];
			$query = "SELECT * FROM SSDD WHERE DoctorID = '$did';";
			$result = mysqli_query($db3, $query);		
			
			while($row = mysqli_fetch_assoc($result))
			{
			$prod_name = $row['PatientID'];
				echo "<option value='$prod_name'>$prod_name</option>";
			}
			?>
		</select> <br>
	  </div>

	  <!--Date Input-->
	  <div class="input-group">
	    <label><b>Date</b></label>
		<input type="datetime-local" class="form-control" name="date">
	  </div>

	  <!--Description Input-->
	  <div class="input-group">
	    <label><b>Description</b></label>
		<input type="text" class="form-control" name="diadesc" required>
	  </div>
	  
	  <!--Codes Input-->
	  <div class="input-group">
		<label><b>Codes (Comma seperated)</b></label>
		<input type="text" class="form-control" name="arr" required>
	  </div>

	  <!--Submit Button-->
	  <div class="input-group">
		<button type="submit" class="w3-button w3-round w3-blue w3-ripple" name="select_diagnosis">Submit New Diagnosis</button>
	  </div>
	  
	</form>

</body>
</html>