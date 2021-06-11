<?php 
  session_start(); 

  if (!isset($_SESSION['success'])) {
  	$_SESSION['msg'] = "You must log in first";
  	header('location: DoctorPortalLogin.php');
  }
  if (isset($_GET['logout'])) {
    session_destroy();
    session_unset();
  	header("location: DoctorPortalIndex.php");
  }
?>

<? include('server.php') ?>

<!--Application landing page here-->
<!DOCTYPE html>
<html lang="en">

<head>
  <title>Welcome to My Health Portal</title>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue.css">
  <link href="css/doctorPortal.css" rel='stylesheet'>
  <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
</head>

<body>
  <!--Navbar-->
  <div class="w3-bar w3-theme w3-large">
    <a href="DoctorPortalIndex.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">HOME</a>
    <div class="nav-right">
      <a href="DoctorPortalIndex.php?logout='1'" class="w3-bar-item w3-button w3-hide-small w3-hover-white">LOGOUT</a>
    </div>
  </div>

  <!--Header-->
  <div class="header w3-theme-d4">
    <h1><b><?php echo "Welcome " . $_SESSION["name"] . ""?></b></h1>
  </div>

  <!--Diagnosis Button-->
  <div class="home-buttons">
    <a class="w3-button w3-xlarge w3-round w3-blue w3-ripple" href="DoctorPortalDiagnosis.php">Diagnose Patients</a>
  </div>

  <!--Prescriptions Button-->
  <div class="home-buttons">
    <a class="w3-button w3-xlarge w3-round w3-blue w3-ripple" href="DoctorPortalPrescriptions.php">Prescriptions</a>
  </div>

  <!--Services Button-->
  <div class="home-buttons">
    <a class="w3-button w3-xlarge w3-round w3-blue w3-ripple" href="DoctorPortalServices.php">Services</a>
  </div>

  <!--Charges Button-->
  <div class="home-buttons">
    <a class="w3-button w3-xlarge w3-round w3-blue w3-ripple" href="DoctorPortalCharges.php">View Charges</a>
  </div>

  <!--Reports Button-->
  <div class="home-buttons">
    <a class="w3-button w3-xlarge w3-round w3-blue w3-ripple" href="DoctorPortalReports.php">View Reports</a>
  </div>
    
  </div>
</body>

</html>