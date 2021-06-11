<?php

error_reporting(0);
include 'includes/dbconn.php';

session_start();

date_default_timezone_set("America/Los_Angeles");

//========== Initializing Variables ==========

//For Regstration
$firstname = "";
$lastname = "";
$email = "";
$genoption = "";
$phone = "";
$dob = "";
$dob_del = "";
$address = "";

//For Diagnosis
$diadesc = "";

//For Prescriptions
$prescription ="";

//For Charges
$charges = "";

//General
$errors 	= array();
$pid	= "";
$did	= "";
$date = "";


//========== Database Connection ==========
$db = new mysqli($servername, $username, "", "db1", $sqlport, $socket);

// Check connection
if ($db->connect_error) {
    die("Connection failed: " . $db->connect_error);
}

$db2 = new mysqli($servername, $username, "", "db2", $sqlport, $socket);

// Check connection
if ($db2->connect_error) {
    die("Connection failed: " . $db2->connect_error);
}

$db3 = new mysqli($servername, $username, "", "db3", $sqlport, $socket);

// Check connection
if ($db3->connect_error) {
    die("Connection failed: " . $db3->connect_error);
}

$db4 = new mysqli($servername, $username, "", "db4", $sqlport, $socket);

// Check connection
if ($db4->connect_error) {
    die("Connection failed: " . $db4->connect_error);
}

//========== Server Functions ==========

// REGISTER USER
if (isset($_POST['reg_user'])) {

  // receive all input values from the form
  $email = $db2->real_escape_string($_POST['email']);
  $password_1 = $db2->real_escape_string($_POST['password_1']);
  $password_2 = $db2->real_escape_string($_POST['password_2']);
  $firstname = $db2->real_escape_string($_POST['firstname']);
  $lastname = $db2->real_escape_string($_POST['lastname']);
  $phone = $db2->real_escape_string($_POST['phone']);

  // get gender radio option
  $genradio = $_POST['gender'];
  switch ($genradio) {
    case "M":
    case "F":
    case "O":
      $genoption = $genradio;
      break;
    default:
      $genoption = "";
  }
  $gender = $db2->real_escape_string($genoption);

  $dob_del = $db2->real_escape_string($_POST['DoB']);
  $dob = str_replace("-", "", $dob_del);

  $address = $db2->real_escape_string($_POST['address']);

  // form validation to ensure all fields have data
  if (empty($email)) { array_push($errors, "An Email is required"); }
  if (empty($password_1)) { array_push($errors, "A Password is required"); }
  if (empty($firstname)) { array_push($errors, "A First Name is required"); }
  if (empty($lastname)) { array_push($errors, "A Last Name is required"); }
  if (empty($phone)) { array_push($errors, "A Phone Number is required"); }
  if (empty($dob)) { array_push($errors, "A Date of Birth is required"); }
  if (empty($address)) { array_push($errors, "An Address is required"); }

  // password confirmation
  if ($password_1 != $password_2) { array_push($errors, "The two passwords do not match"); }

  // check that a user does not already exist with the same email
  $result = $db2->query("SELECT * FROM DoctorLogin WHERE Email='$email' LIMIT 1;");
  $user = mysqli_fetch_assoc($result);

  // check for a conflicting email
  if ($user) {
    array_push($errors, "An account with this email already exists");
  }

  // if there are no errors, add the doctor's account information
  if (count($errors) == 0) {
  	//TODO $password = md5($password_1); //encrypt the password before saving in the database
	  $name = $firstname . " " . $lastname;

    // insert the login data
    $db2->query("INSERT INTO DoctorLogin (Email, Pass) VALUES ('$email', '$password_1');");

    // get the id generated for this account
    $didquery = $db2->query("SELECT DID FROM DoctorLogin WHERE Email='$email' AND Pass='$password_1';");
    $doc = mysqli_fetch_assoc($didquery);
    $did = $doc['DID'];

    // insert the rest of the doctor data
    $db2->query("INSERT INTO DoctorData (DID, Fname, Lname, DOB, Gender, Address, Email, Phone)
                VALUES ('$did', '$firstname', '$lastname', '$dob', '$gender', '$address', '$email', '$phone');");

    // set session variables after registration
  	$_SESSION['name'] = "$name";
  	$_SESSION['success'] = "You are now logged in";
	$_SESSION['did'] = "$did";

  	header('location: DoctorPortalIndex.php');
  }
}

// LOGIN USER
if (isset($_POST['login_user'])) {
  $email = $db2->real_escape_string($_POST['email']);
  $password = $db2->real_escape_string($_POST['password']);

  // if there are no errors with input
  if (count($errors) == 0) {

  	// query the database for a matching user
    $results = $db2->query("SELECT Fname, Lname FROM DoctorLogin RIGHT JOIN DoctorData ON DoctorLogin.DID = DoctorData.DID
                            WHERE DoctorLogin.Email = '$email' and DoctorLogin.Pass = '$password';");

    // check if a valid user is found
    if($results->num_rows == 1) {

      // save session details
      $name = mysqli_fetch_assoc($results);
      $fname = $name['Fname'];
      $lname = $name['Lname'];
      $_SESSION['success'] = "You are now logged in";
      $_SESSION['name'] = $fname . " " . $lname;

	  //Grab DoctorID
	  $query = "SELECT DID FROM DoctorLogin WHERE Email='$email' LIMIT 1;";
	  $results = mysqli_query($db2, $query);
	  $assoc = mysqli_fetch_assoc($results);
	  $did = $assoc['DID'];
	  $_SESSION['did'] = "$did";

      // navigate to home page
	    header('location: DoctorPortalIndex.php');
    }
    else {
      array_push($errors, "The email or password was invalid");
    }
  }
}



//Diagnosis
if (isset($_POST['select_diagnosis'])) {
  $pid = mysqli_real_escape_string($db, $_POST['pid']);
  $did = $_SESSION['did'];
  $date = mysqli_real_escape_string($db, $_POST['date']);
  $diadesc = mysqli_real_escape_string($db, $_POST['diadesc']);
  $arr = mysqli_real_escape_string($db, $_POST['arr']);

  //Check empties
  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }
  if (empty($did)) {
  	array_push($errors, "The doctor id is required");
  }
  if (empty($date)) {
  	$date = date("Y-m-d H:i:s");
  }
  else
  {
	  $date = date("Y-m-d H:i:s", strtotime($date));
  }
  if (empty($diadesc)) {
  	array_push($errors, "A description is required here");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  //Check valid Doctor Patient Request
  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }

  if($sscheck['DoctorID'] != $did) {
	  array_push($errors, "You are not the assigned doctor for this patient.");
  }

  //Submit data
  if (count($errors) == 0) {
	$a = str_getcsv($arr);

	$query = "INSERT INTO Diagnosis(PID, DID, DiaDate, DiaDesc) VALUES ('$pid', '$did', '$date', '$diadesc');";

	mysqli_query($db2, $query);


	$check_query = "SELECT * FROM Diagnosis WHERE DiaDate='$date' LIMIT 1;";
	$result = mysqli_query($db2, $check_query);
	$sscheck = mysqli_fetch_assoc($result);

	$diaID = $sscheck['DiaID'];

   //Adding into PatientRecords
   $query = "INSERT INTO PatientRecords(PID, DID, Date, Type, TypeID, Description) VALUES ('$pid', '$did', '$date', 'Diagnosis', '$diaID', '$diadesc' );";
   mysqli_query($db, $query);

	foreach($a as $code) {
		$query = "INSERT INTO Codes(DiaID, Code) VALUES ('$diaID', '$code');";
		mysqli_query($db2, $query);
	}

	echo success;
  }
}



//Submit Prescriptions
if (isset($_POST['select_prescription'])) {
	$pid = mysqli_real_escape_string($db, $_POST['pid']);
  $did = $_SESSION['did'];
  $date = mysqli_real_escape_string($db, $_POST['date']);
  $prescription = mysqli_real_escape_string($db, $_POST['prescription']);

  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }
  if (empty($did)) {
  	array_push($errors, "The doctor id is required");
  }
  if (empty($date)) {
  	$date = date("Y-m-d");
  }
  if (empty($prescription)) {
  	array_push($errors, "A description is required");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }

  if($sscheck['DoctorID'] != $did) {
	  array_push($errors, "You are not the assigned doctor for this patient.");
  }
  if (count($errors) == 0) {

	$query = "INSERT INTO Prescriptions(PID, DID, PreDate, Prescription) VALUES ('$pid', '$did', '$date', '$prescription');";
	mysqli_query($db2, $query);

   //Adding into Patient Records
   $check_query = "SELECT * FROM Prescriptions WHERE PreDate='$date' LIMIT 1;";
	$result = mysqli_query($db2, $check_query);
	$sscheck = mysqli_fetch_assoc($result);

	$preid = $sscheck['PreID'];
   //CURRENTLY FAILING: sscheck is empty, probably because we have date and datetime
   $query = "INSERT INTO PatientRecords(PID, DID, Date, Type, TypeID, Description) VALUES ('$pid', '$did', '$date', 'Prescription', '$preid', '$prescription');";
   mysqli_query($db, $query);

	$query = "SELECT CostPerUnit FROM MedProdCosts natural join MedicalProducts WHERE Name='$prescription' LIMIT 1;";
	$result = mysqli_query($db2, $query);
	$assoc = mysqli_fetch_assoc($result);
	print_r($assoc);

	$charge = $assoc['CostPerUnit'];
	$query = "INSERT INTO Charges(PID,CharDate,Charge,Reason) VALUES ('$pid', '$date', '$charge', '$prescription')";
	mysqli_query($db2, $query);

   //Adding into Patient Records
   $check_query = "SELECT * FROM Charges WHERE PID='$pid' AND CharDate='$date' AND Charge='$charge' AND Reason='$prescription' LIMIT 1;";
   $result = mysqli_query($db2, $check_query);
   $sscheck = mysqli_fetch_assoc($result);

   $typeid = $sscheck['ChargeID'];

   $query = "INSERT INTO PatientRecords(PID, DID, Date, Type, TypeID, Description, Price) VALUES ('$pid', '$did', '$date', 'Charge', '$typeid', '$prescription', '$charge' );";
   mysqli_query($db, $query);

	echo success;
  }
}

//Submit Services
if (isset($_POST['select_service'])) {
	$pid = mysqli_real_escape_string($db, $_POST['pid']);
  $did = $_SESSION['did'];
  $date = mysqli_real_escape_string($db, $_POST['date']);
  $service = mysqli_real_escape_string($db, $_POST['service']);

  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }
  if (empty($did)) {
  	array_push($errors, "The doctor id is required");
  }
  if (empty($date)) {
  	$date = date("Y-m-d");
  }
  if (empty($service)) {
  	array_push($errors, "A description is required");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }

  if($sscheck['DoctorID'] != $did) {
	  array_push($errors, "You are not the assigned doctor for this patient.");
  }
  if (count($errors) == 0) {
   echo $date;
	//Insert Service into Service Log
	$query = "INSERT INTO Services(PID, DID, SerDate, Service) VALUES ('$pid', '$did', '$date', '$service');";
	mysqli_query($db2, $query);

   //Adding into Patient Records
   $check_query = "SELECT * FROM Services WHERE SerDate='$date' LIMIT 1;";
	$result = mysqli_query($db2, $check_query);
	$sscheck = mysqli_fetch_assoc($result);

	$serid = $sscheck['SerID'];

   $query = "INSERT INTO PatientRecords(PID, DID, Date, Type, TypeID, Description) VALUES ('$pid', '$did', '$date', 'Service', '$serid', '$service' );";
   mysqli_query($db, $query);

	//Check Patient Hospital
	$query = "SELECT HospitalID FROM SSDH WHERE PatientID = $pid;";
	$result = mysqli_query($db3, $query);
	$assoc = mysqli_fetch_assoc($result);
	//print_r($assoc);
	$hid = $assoc['HospitalID'];

	//Select charge for service from hospital
	$query = "SELECT Charge FROM HospitalCharges natural join ServicesOffered WHERE Description='$service' and HID='$hid' LIMIT 1;";
	$result = mysqli_query($db2, $query);
	$assoc = mysqli_fetch_assoc($result);
	//print_r($assoc);
	$charge = $assoc['Charge'];

	//Insert charge into charge table
	$query = "INSERT INTO Charges(PID,CharDate,Charge,Reason) VALUES ('$pid', '$date', '$charge', '$service')";
	mysqli_query($db2, $query);

   //Adding into Patient Records
   $check_query = "SELECT * FROM Charges WHERE PID='$pid' AND CharDate='$date' AND Charge='$charge' AND Reason='$service' LIMIT 1;";
   $result = mysqli_query($db2, $check_query);
   $sscheck = mysqli_fetch_assoc($result);

   $typeid = $sscheck['ChargeID'];

   $query = "INSERT INTO PatientRecords(PID, DID, Date, Type, TypeID, Description, Price) VALUES ('$pid', '$did', '$date', 'Charge', '$typeid', '$service', '$charge' );";
   mysqli_query($db, $query);

	echo success;
  }
}

//Print Charges
if (isset($_POST['select_charges'])) {
  $pid = mysqli_real_escape_string($db, $_POST['pid']);

  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }

  if (count($errors) == 0) {
	  $query = "SELECT SUM(Charge) AS total_charge FROM Charges WHERE PID = $pid;";
	  $result = mysqli_query($db2, $query);
    $assoc = mysqli_fetch_assoc($result);
    $charges = $assoc['total_charge'];
	  //print_r($assoc);
    //echo success;
  }
}

//Print Report
if (isset($_POST['select_reports'])) {
  /*$pid = mysqli_real_escape_string($db, $_POST['pid']);

  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }*/
  $qhold = mysqli_real_escape_string($db, $_POST['Queries']);
  $sqhold = mysqli_real_escape_string($db, $_POST['SubQuery']);
  $operator = mysqli_real_escape_string($db, $_POST['Operator']);
  $serYear = mysqli_real_escape_string($db, $_POST['Year']);
  $conj = "WHERE";
  /*echo $qhold;
  echo nl2br("\n");
  echo $sqhold;*/
  //echo $serYear;
	if($operator == "SUM")
	{
		$seroper = "SUM(Charge) as A, PID";
	}
	else if($operator == "COUNT")
	{
		$seroper = "COUNT(Charge) as A, PID";
	}
	else
	{
		$seroper = "*";
	}

	if($qhold == "default")
	{
		$query = "SELECT ".$seroper." FROM Charges";
	}
	else if($qhold == "MedicalProducts")
	{
		$query = "SELECT ".$seroper. " FROM Charges ".$conj." Reason='$sqhold'";
		$conj = "AND";
	}
	else if($qhold == "ServicesOffered")
	{
		$query = "SELECT ".$seroper." FROM Charges ".$conj." Reason='$sqhold'";
		$conj = "AND";
	}
	else
	{
		$query = "SELECT SUM(Charge) FROM Charges";
	}
	if($serYear != "All")
	{
		if($qhold == "MedicalProducts" || $qhold == "ServicesOffered")
		{
			$query = $query.$conj." YEAR(CharDate)=$serYear";
		}
		else
		{
			$query = $query.$conj." YEAR(CharDate)=$serYear";
			$conj = "AND";
		}
	}
	//$query = $query." ".$conj." YEAR(CharDate)=$serYear";
	$did=$_SESSION['did'];
	//echo $did;
	$queryPID = "SELECT PatientID as PID FROM SSDD WHERE DoctorID='$did'";
	$queryPIDr = mysqli_query($db3, $queryPID);
	//echo "Hello";
	//print_r($queryPIDr);
	//echo nl2br("\n");
	//echo $queryPIDr;
	//$assoc = (mysqli_fetch_assoc($queryPIDr));
	//print_r($assoc);
	//echo nl2br("\n");
	//echo "test";
	
  

  if (count($errors) == 0) {  
	$assoc2 = (mysqli_fetch_assoc($queryPIDr));
	$PIDtest = $assoc2['PID'];
	$querymiddle = $query." ".$conj." PID = $PIDtest";
	//echo $querymiddle;
	//echo nl2br("\n");
	while($assoc2 = (mysqli_fetch_assoc($queryPIDr)))
	{
		$unionbool=true;
		$PIDtest = $assoc2['PID'];
		$querymiddle = $querymiddle." UNION ".$query." ".$conj." PID = $PIDtest";
		//echo $querymiddle;
	}
	if($unionbool && $seroper != "*")
	{
		$queryfinal = "SELECT SUM(A) FROM ( $querymiddle ) as tbl";
	}
	else
	{
		$queryfinal = $querymiddle;
	}
	/*echo nl2br("\n");
	echo nl2br("\n");
	echo $queryfinal;*/
	$result = mysqli_query($db2, $queryfinal);
	
  }
}
//Antiquated (Kept for emergency)
/*//Submit Diagnosis
if (isset($_POST['select_diagnosis'])) {
  $pid = mysqli_real_escape_string($db, $_POST['pid']);
  $did = mysqli_real_escape_string($db, $_POST['did']);
  $date = mysqli_real_escape_string($db, $_POST['date']);
  $diadesc = mysqli_real_escape_string($db, $_POST['diadesc']);

  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }
  if (empty($did)) {
  	array_push($errors, "The doctor id is required");
  }
  if (empty($date)) {
  	array_push($errors, "A date is required");
  }
  if (empty($diadesc)) {
  	array_push($errors, "A description is required");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }

  if($sscheck['DoctorID'] != $did) {
	  array_push($errors, "You are not the assigned doctor for this patient.");
  }
  if (count($errors) == 0) {

	$query = "INSERT INTO Diagnosis(PID, DID, DiaDate, DiaDesc) VALUES ('$pid', '$did', '$date', '$diadesc');";
	mysqli_query($db2, $query);
	echo success;
  }
}*/

/*//Submit Prescriptions Antiquated (Kept for emergency)
if (isset($_POST['select_prescription'])) {
	$pid = mysqli_real_escape_string($db, $_POST['pid']);
  $did = mysqli_real_escape_string($db, $_POST['did']);
  $date = mysqli_real_escape_string($db, $_POST['date']);
  $prescription = mysqli_real_escape_string($db, $_POST['prescription']);

  if (empty($pid)) {
  	array_push($errors, "The patient id is required");
  }
  if (empty($did)) {
  	array_push($errors, "The doctor id is required");
  }
  if (empty($date)) {
  	array_push($errors, "A date is required");
  }
  if (empty($prescription)) {
  	array_push($errors, "A description is required");
  }

  $check_query = "SELECT * FROM SSDD WHERE PatientID='$pid' LIMIT 1;";
  $result = mysqli_query($db3, $check_query);
  $sscheck = mysqli_fetch_assoc($result);

  if(!($sscheck)) {
	  array_push($errors, "This patient has not been registered. Please check the PID.");
  }

  if($sscheck['DoctorID'] != $did) {
	  array_push($errors, "You are not the assigned doctor for this patient.");
  }
  if (count($errors) == 0) {

	$query = "INSERT INTO Prescriptions(PID, DID, PreDate, Prescription) VALUES ('$pid', '$did', '$date', '$prescription');";
	mysqli_query($db2, $query);
	echo success;
  }
}*/

?>
