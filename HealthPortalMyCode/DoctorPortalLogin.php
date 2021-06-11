<?php include('server.php') ?>

<!DOCTYPE html>
<html lang="en">

<head>
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Doctor Login</title>
  <link rel="stylesheet" href="https://www.w3schools.com/lib/w3-theme-blue.css">
  <link href="css/doctorPortal.css" rel='stylesheet'>
  <link rel="stylesheet" href="https://www.w3schools.com/w3css/4/w3.css">
</head>

<body>
  <!--Navbar-->
  <div class="w3-bar w3-theme w3-left-align w3-large">
    <a href="index.php" class="w3-bar-item w3-button w3-hide-small w3-hover-white">PORTAL SELECTION</a>
  </div>

  <!--Header-->
  <div class="header w3-theme-d4">
    <h1><b>My Doctor Health Portal</b></h1>
  </div>

  <!--Form Errors-->
  <div class="login-error center">
	<b><?php include('errors.php'); ?></b>
  </div>

  <!--Login Button-->
  <div class="home-middle">
    <button class="w3-button w3-xlarge w3-round w3-blue w3-ripple" 
    onclick="document.getElementById('loginForm').style.display='block'" style="width:auto;"
    id="btnLogin" type="submit" name="login">Login  
    </button>
  </div>

  <!--Signup Button-->
  <div class="home-middle">
    <button class="w3-button w3-xlarge w3-round w3-blue w3-ripple" 
    onclick="document.getElementById('signupForm').style.display='block'" style="width:auto;"
    id="btnSignup" type="submit" name="signup">Create Account
    </button>
  </div>

  <!--Login Popup Form-->
  <div id="loginForm" class="modal">
	<form class="modal-content" method="post" action="<?php echo htmlspecialchars($_SERVER['PHP_SELF']);?>">
	  <h2>Doctor Login</h2>

	  <!--Login Email Input-->
	  <div class="input-group">
		<input type="text" class="form-control" name="email" placeholder="email" required autofocus>
	  </div>

	  <!--Login Pass Input-->
	  <div class="input-group">
		<input type="password" class="form-control" name="password" placeholder="password" required>
	  </div>

	  <!--Login Submit Button-->
	  <div class="input-group">
		<button type="submit" class="w3-button w3-round w3-blue w3-ripple" name="login_user">Login</button>
	  </div>

	  <!--Login Cancel Button-->
	  <div class="input-group">
        <button type="button" onclick="document.getElementById('loginForm').style.display='none'" class="w3-button w3-round w3-red">Cancel</button>
	  </div>
	  
	</form>
  </div>

  <!--Register Popup Form-->
  <div id="signupForm" class="modal">
	<form class="modal-content" method="post" action="<?php echo htmlspecialchars($_SERVER['PHP_SELF']);?>">
	  <h2>Doctor Signup</h2>
	
	<!--Register Email Input-->
	<div class="input-group">
	  <label><b>Email</b></label>
  	  <input type="email" class="form-control" name="email"  placeholder="email" required autofocus>
	</div>
	
	<!--Register Password Input-->
  	<div class="input-group">
	  <label><b>Password</b></label>
  	  <input type="password" class="form-control" name="password_1" placeholder="password" required>
	</div>
  	<div class="input-group">
	  <label><b>Confirm Password</b></label>
  	  <input type="password" class="form-control" name="password_2" placeholder="confirm password" required>
	</div>
	
	<!--Registration Fields-->
	<div class="input-group">
	  <label><b>First Name</b></label>
  	  <input type="text" name="firstname" placeholder="first name">
  	</div>
	<div class="input-group">
	  <label><b>Last Name</b></label>
  	  <input type="text" name="lastname" placeholder="last name">
  	</div>
	<div class="input-group">
  	  <label><b>Phone Number</b></label>
  	  <input type="number" name="phone" placeholder="xxxxxxxxxx" value="<?php echo $phone; ?>" min="1000000000" max="9999999999">
	  </div>
	<div class="input-group">
  	  <label><b>Gender</b></label><br>
	  <input type="radio" id="male" name="gender" value="M">
	    <label for="male">Male</label><br>
	  <input type="radio" id="female" name="gender" value="F">
	    <label for="female">Female</label><br>
	  <input type="radio" id="other" name="gender" value="O">
	    <label for="other">Other</label><br>
  	</div>
	<div class="input-group">
  	  <label><b>Date of Birth</b><label>
	  <input type="date" name="DoB" value="<?php echo $dob; ?>">
  	</div>
	<div class="input-group">
	  <label><b>Address</b><label>
  	  <input type="text" name="address" placeholder="address" value="<?php echo $address; ?>">
	</div>
	  
	<!--Register Submit Button-->
  	<div class="input-group">
  	  <button type="submit" class="w3-button w3-round w3-blue w3-ripple" name="reg_user">Register</button>
	</div>
	  
	<!--Register Cancel Button-->
	<div class="input-group">
      <button type="button" onclick="document.getElementById('signupForm').style.display='none'" class="w3-button w3-round w3-red">Cancel</button>
	</div>
	</form>
  </div>

  <!--Footer-->
  <div class="footer center w3-theme-d4">
    <h3>CS360 Spring 2021</h3>
  </div>

  <!--Close Popup Script-->
  <script>
	// get the login and signup popups
	var modals = document.getElementsByClassName('modal');

	// rehide modals when clicked outside
	window.onclick = function(event) {
	  for(i=0; i<modals.length;i++) {
		if (event.target == modals[i]) {
		  modals[i].style.display = "none";
		}
	  }
	}
  </script>
  
</body>

</html>