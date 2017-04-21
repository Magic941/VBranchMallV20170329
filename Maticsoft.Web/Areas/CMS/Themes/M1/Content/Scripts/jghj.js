	function xiala(x,y){
		gb(x);
		if(document.getElementById(x).style.display!="block")
	  {
		  document.getElementById(x).style.display='block';
	   document.getElementById(y).className='ann2';
	  
	  }
	  else
	  {
		 document.getElementById(x).style.display='none';
  document.getElementById(y).className='ann1';
	  }
	  
 		}
 function gb(x){
 for(i=1;i<7;i++)
 {
	  if(document.getElementById("nav"+i))
	 {
	 if(document.getElementById("nav"+i).style.display!="none"&&"nav"+i!=x)
	  {
	 
 document.getElementById("nav"+i).style.display='none';
  document.getElementById("class"+i).className='ann1';
	  }
	 }
 }
 }
	
	
	function erjixiala(a0,a,b){
		erjigb(a0,a,b);
		

		if(document.getElementById(a0+a+"_u"+b).style.display!="block")
	  {
		document.getElementById(a0+a+"_u"+b).style.display="block";
	   document.getElementById(a0+a+"_erji"+b).className="erji2";
	  
	  }
	  else
	  {
	document.getElementById(a0+a+"_u"+b).style.display="none";
  document.getElementById(a0+a+"_erji"+b).className="erji1";
	  }
		//alert(document.getElementById(a0+a).style.display);
 		}
		
		
 function erjigb(a0,a,b){
 for(i=1;i<9;i++)
 {
	 if(document.getElementById(a0+a+"_u"+i))
	 {
		if(document.getElementById(a0+a+"_u"+i).style.display!="none"&&i!=b)
		{
   document.getElementById(a0+a+"_u"+i).style.display='none';
	document.getElementById(a0+a+"_erji"+i).className='erji1';
		}
	 }
 }
	
	
 }
