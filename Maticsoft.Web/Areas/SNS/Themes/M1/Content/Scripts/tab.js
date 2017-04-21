// JavaScript Document

function xuanxiangka(objct)
{
	function getByClass(oparent,sclass)
	{		
		var aEle=oparent.getElementsByTagName('*');	
		var result=[];
		for(var j=0;j<aEle.length;j++)
		{
			if(aEle[j].className==sclass)
			{
				result.push(aEle[j]);	
			}
		}
		return result;	
	};
	var oTab=document.getElementById(objct);
	var aLi=oTab.getElementsByTagName('li');
	var aShow=getByClass(oTab,'content');
	for(var i=0;i<aLi.length;i++)
	{
		aLi[i].index=i;
		aLi[i].onclick=function()
		{
			for(var i=0;i<aLi.length;i++)
			{
				aLi[i].className='';
				aShow[i].style.display='none';	
			}
			this.className='active';
			aShow[this.index].style.display='block';	
		};			
	}	
};