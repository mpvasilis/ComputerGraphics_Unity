<?php

ini_set('display_errors', 1);
ini_set('display_startup_errors', 1);
error_reporting(E_ALL);

require 'includes/db.php';
require_once 'includes/functions.php';
$user = new user;

if (!($user->LoggedIn())) {
	die('You need to login.');
}

//$data_class = htmlspecialchars($_GET['data_class']);
$country    = htmlspecialchars($_GET['country']);
$poll_str   = htmlspecialchars($_GET['poll_str']);

header('Content-Type: application/json');

$codes = array();
$SQLGetLogs = $odb->prepare("SELECT * FROM `components`");
$SQLGetLogs->execute();
while ($getInfo = $SQLGetLogs->fetch(PDO::FETCH_ASSOC)) {
	$code=intval(trim($getInfo["code"]));
	$codetoname[$code]=$getInfo["abbreviation"];
	array_push($codes, $code);		
}
if(in_array($poll_str,$codes)){
?>

{"title":"Airbase","type":"marker","locations":
<?php

$locations     = array();


	$country_query="";
	if($country=="All Countries" || $country==""){
			$country_query=""; 
	}
	else{
			$country_query=" AND `path` LIKE '%$country%' ";
	}

	$stressor_query="";
	if($poll_str=="All Chemicals" || $poll_str=="All Stressors" || $poll_str==""){
			$stressor_query=""; 
	}
	else{
			$stressor_query=" AND `path` LIKE '%$poll_str%' ";
	}

	$SQLGetLogs = $odb->prepare("SELECT * FROM data JOIN Airbase_Stations ON Airbase_Stations.station_european_code = data.station_code WHERE `id` > 0 $country_query $stressor_query GROUP BY station_code");
	$SQLGetLogs->execute();
	while ($getInfo = $SQLGetLogs->fetch(PDO::FETCH_ASSOC)) {
		$location                = array();
		$station                 = $getInfo['station_code'];
		if ($getInfo['station_city'] == "") {
			$getInfo['station_city'] = "-";
			$path                    = $getInfo['path'];
			$path                    = explode("/", $path);
			$compoment               = substr($path[4], 7, 5);
		}
			$compoment_rounded = round($poll_str); 
					$location["lat"]   = $getInfo['station_latitude_deg'];
					$location["lon"]   = $getInfo['station_longitude_deg'];
					$location["title"] = $getInfo['station_european_code'];
					$location["html"]  = "<strong><h6>" . $getInfo['station_code'] . "</h6></strong><strong>Country: </strong>" . $getInfo['country_name'] . "<br><strong>City: </strong>" . ucfirst(strtolower($getInfo['station_city'])) . "<br><strong>Longitude: </strong>" . $getInfo['station_longitude_deg'] . "<br><strong>Latitude: </strong>" . $getInfo['station_latitude_deg'] . "<br><strong>Source: </strong>EEA Airbase<br><strong>Data: </strong>"."<a onclick='loadData(`$station`,$compoment_rounded,this.innerHTML)' class='modal-trigger'>".$codetoname[$compoment_rounded]."</sub></a>&nbsp;";
					array_push($locations, $location);		

	}


    echo json_encode($locations);
    ?>
    ,"count":<?php
    echo count($locations)."}";
}
?>
<?php
if($poll_str=="" || $poll_str=="All Chemicals"){
	$locations     = array();

?>
{"title":"Airbase","type":"marker","locations":
<?php
$SQLGetLogs = $odb->prepare("SELECT * FROM data JOIN Airbase_Stations ON Airbase_Stations.station_european_code = data.station_code");
$SQLGetLogs->execute();
while ($getInfo = $SQLGetLogs->fetch(PDO::FETCH_ASSOC)) {
	$station   = $getInfo['station_code'];
	$path      = $getInfo['path'];
	$path      = explode("/", $path);
	$compoment = substr($path[4], 7, 5);
	$compoment_rounded=round($compoment);
    //echo $compoment;
	if (strpos($stations_data[$station], $compoment) === false) {
	
		if (array_key_exists(trim(round($compoment)), $codetoname)) {
			$stations_data[$station] .= "<a onclick='loadData(`$station`,$compoment_rounded,this.innerHTML)' class='modal-trigger'>".$codetoname[round($compoment)]."</sub></a>&nbsp;";
		}
	}
}
	$country_query="";
	if($country=="All Countries" || $country==""){
			$country_query=""; 
	}
	else{
			$country_query=" AND `path` LIKE '%$country%' ";
	}
	$SQLGetLogs = $odb->prepare("SELECT * FROM data JOIN Airbase_Stations ON Airbase_Stations.station_european_code = data.station_code WHERE `id` > 0 $country_query GROUP BY station_code");
	$SQLGetLogs->execute();
	while ($getInfo = $SQLGetLogs->fetch(PDO::FETCH_ASSOC)) {
		$location                = array();
		$station                 = $getInfo['station_code'];
		$stations_data[$station] = implode('&nbsp;', array_unique(explode('&nbsp;', $stations_data[$station])));
		if ($getInfo['station_city'] == "") {
			$getInfo['station_city'] = "-";
			$path                    = $getInfo['path'];
			$path                    = explode("/", $path);
			$compoment               = substr($path[4], 7, 5);
		}
		if (strlen($stations_data[$station]) > 10) {

			$location["lat"]   = $getInfo['station_latitude_deg'];
			$location["lon"]   = $getInfo['station_longitude_deg'];
			$location["title"] = $getInfo['station_european_code'];
			$location["html"]  = "<strong><h6>" . $getInfo['station_code'] . "</h6></strong><strong>Country: </strong>" . $getInfo['country_name'] . "<br><strong>City: </strong>" . ucfirst(strtolower($getInfo['station_city'])) . "<br><strong>Longitude: </strong>" . $getInfo['station_longitude_deg'] . "<br><strong>Latitude: </strong>" . $getInfo['station_latitude_deg'] . "<br><strong>Source: </strong>EEA Airbase<br><strong>Data: </strong>" . $stations_data[$station] . "";
			array_push($locations, $location);


		}
	}
    echo json_encode($locations);
    ?>
    ,"count":<?php
    echo count($locations)."}";
}
?>
