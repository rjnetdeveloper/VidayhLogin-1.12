<?php
 if(isset($_POST['submit']))
{
$to = 'deepak@monteage.in. yogesh@monteage.in';

$subject = 'SunTrust Pte Ltd Enquiries';
$message = '
   <strong>Name: </strong> '.$_POST['SchoolName'].' <br/><br/>
   <strong>Email: </strong> '.$_POST['Location'].' <br/><br/>
    <strong>Phone: </strong> '.$_POST['Name'].' <br/><br/>
	 <strong>Phone: </strong> '.$_POST['DesignationType'].' <br/><br/>
	  <strong>Phone: </strong> '.$_POST['MobileNo'].' <br/><br/>
	   <strong>Phone: </strong> '.$_POST['EmailAddress '].' <br/><br/>
	<strong>Message: </strong> '.$_POST['Description'].'
	
  
'; 
$from =$_POST['email'];
$headers  = 'MIME-Version: 1.0' . "\r\n";
$headers .= 'Content-type: text/html; charset=iso-8859-1' . "\r\n";
$headers .= 'From: '.$_POST['name'].'  ' . "\r\n";
$result = mail($to, $subject, $message, $headers);
if($result)
{
?>
<script type="text/javascript">
<!--
alert("Thank You for yor interest. We will get back to you as soon as possible.");
location.href='contact';
//-->
</script>
<?php
}
}

?>