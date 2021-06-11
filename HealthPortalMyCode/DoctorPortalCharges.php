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
	<a class="w3-bar-item w3-hide-small w3-white">Charges</a>
	<a href="DoctorPortalReports.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">Reports</a>
    <div class="nav-right">
      <a href="DoctorPortalIndex.php?logout='1'" class="w3-bar-item w3-button w3-hide-small w3-hover-white">LOGOUT</a>
    </div>
  </div>

  <!--Header-->
  <div class="header w3-theme-d4">
    <h1><b>Charges</b></h1>
  </div>

  <!--Form Errors-->
  <div class="error center">
	<b><?php include('errors.php'); ?></b>
  </div>
	
  <!--View Charges Form-->
  <form id="chargesForm" class="center" method="post" action="<?php echo htmlspecialchars($_SERVER['PHP_SELF']);?>">

    <!--PID input-->
    <div class="input-group">
	  <label><b>PID</b></label>
	  <select name = "pid">
	    <option>Select</option>
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
	  </select>
	</div>

	<!--Submit Button-->
	<div class="input-group">
	  <button type="submit" class="w3-button w3-round w3-blue w3-ripple" name="select_charges">View Patient Charges</button>
	</div>
  </form>

  <!--Charges Display-->
  <div class="home-middle">
    <?php
	  if ($charges != null && $_POST['pid'] != null) {
	    $pid = $_POST['pid'];
		echo "<p><b>Total charges for patient ".$pid."</b></p>";
		echo "<p>$".$charges.".00</p>";
		//print_r($assoc);
	  }
	?>
  </div>

</body>
</html>