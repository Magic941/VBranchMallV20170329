
/* 

  ================================================
  PVII Slide Show scripts
  Copyright (c) 2007 Project Seven Development
  www.projectseven.com
  Version: 1.2.0 - build 4-28b
  ================================================
  
*/

var P7ssma,p7ssmfrst,p7sscur=-1,p7ssmmode='manual',p7ssmtimer,p7ssmbusy=false,p7ssmsobj,p7ofX,p7ofY,p7ssmspd,p7ssmMinSpeed=2,p7ssmMaxSpeed=25;
var p7sstmb=new Array(),P7ssmpnls=new Array(),p7ssmn=false,p7ssmih,p7ssgld,p7ssmAlt;
function P7_setSSM(){
	if(!document.getElementById){
		return;
	}
	var h,uA=navigator.userAgent.toLowerCase();
	if(window.opera){
		if(uA.indexOf("opera 5")>-1 || uA.indexOf("opera 6")>-1){
			return;
		}
	}
	h='\n<st'+'yle type="text/css">\n';
	h+='.p7ssm_thumb_section{display:none;}\n';
	h+='</s'+'tyle>';
	document.write(h);
}
P7_setSSM();
function P7_SSMinit(){
	var i,x,tM,uA,tB,tA,tP,tD,id,d,tC,ds,bT,pV,iV;
	tM=document.getElementById("p7ssm");
	if(!tM){
		return;
	}
	P7ssma=arguments;
	uA=navigator.userAgent.toLowerCase();
	if(window.opera){
		if(uA.indexOf("opera 5")>-1 || uA.indexOf("opera 6")>-1){
			return;
		}
	}
	else if(uA.indexOf("netscape")>-1 || uA.indexOf("konqueror")>-1){
		P7ssma[3]=0;
	}
	if(navigator.appVersion.indexOf("MSIE 5")>-1){
		P7ssma[1]=0;
		P7ssma[3]=0;
	}
	p7ssmfrst=true;
	document.p7sspre=new Array();
	tB=document.getElementById("p7ssm_thumbs");
	tA=tB.getElementsByTagName("A");
	x=0;
	for(i=0;i<tA.length;i++){
		if(tA[i].parentNode.nodeName=="LI"){
			document.p7sspre[x]=new Image();
			document.p7sspre[x].cmp=false;
			document.p7sspre[x].onload=function (){
				this.cmp=true;
			};
			tA[i].p7ssindex=x;
			p7sstmb[x]=tA[i];
			x++;
			tA[i].onmouseover=function (){
				P7_SSMtmb_ovr(this);
			};
			tA[i].onmouseout=function (){
				P7_SSMtmb_out(this);
			};
tA[i].onclick= function(){
	return P7_SSMtmb(this);
};
}
}
tA=document.getElementsByTagName("A");
x=0;
for(i=0;i<tA.length;i++){
id=tA[i].id;
if(id && id.indexOf('p7SSMt')==1){
if(id=='p7SSMthmb' || id=='p7SSMtnav'){
	tA[i].onclick=function (){
		return P7_SSMtrgPanel(this.id,0);
	};
}
else{
	tA[i].onclick=function (){
		return P7_SSMtrgPanel(this.id,1);
	};
}
d=id.replace("t","w");
tD=document.getElementById(d);
if(tD){
	tC=tD.getElementsByTagName("DIV")[0];
	if(tA[i].className&&tA[i].className=="down"){
		tA[i].p7state="open";
	}
	else{
		tA[i].p7state="closed";
		tC.style.display="none";
	}
	P7ssmpnls[x]=id;
	x++;
	tA[i].p7type='normal';
	tA[i].p7panel=d;
	if(id.indexOf('_')>-1){
		tA[i].p7type='toggle';
	}
}
}
}
if(P7ssma[6]==1){
p7ssmmode='auto';
}
else{
p7ssmmode='manual';
}
bT=document.getElementById('p7ssm_pp');
bT.onclick=function(){
return P7_SSMppTrig(this);
};
if(P7ssma[6]==1){
P7_SSMplay(1);
}
else{
P7_SSMpause(1);
}
tB=document.getElementById('p7ssm_first');
tB.onclick=function(){
return P7_SSMctrl('first');
};
tB=document.getElementById('p7ssm_last');
tB.onclick=function(){
return P7_SSMctrl('last');
};
tB=document.getElementById('p7ssm_prev');
tB.onclick=function(){
return P7_SSMctrl('previous');
};
tB=document.getElementById('p7ssm_next');
tB.onclick=function(){
return P7_SSMctrl('next');
};
P7_SSMsetDelay();
document.onmousedown=P7_SSMeng;
document.onmousemove = P7_SSMdrg;
document.onmouseup = P7_SSMrel;
pV=document.getElementById("p7ssm_preview");
iV=P7_getChildIm(pV);
iV.busy=false;
if(iV){
iV.removeAttribute("width");
iV.removeAttribute("height");
}
tA=document.getElementById("p7ssm_fslink");
if(tA){
tA.onclick=function(){
return P7_imgClick(this);
};
}
p7ssmn=true;
x=P7ssma[5];
P7_SSMtrig(x);
P7_SSMurl();
}
function P7_SSMurl(){
	var i,h,s,x,d='ssm';
	if(document.getElementById){
		h=document.location.search;
		if(h){
			h=h.replace('?','');
			s=h.split(/[=&]/g);
			if(s&&s.length){
				for(i=0;i<s.length;i+=2){
					if(s[i]==d){
						x=s[i+1];
						if(x){
							P7_SSMtrig(x);
						}
					}
				}
			}
		}
		h=document.location.hash;
		if(h){
			x=h.substring(1,h.length);
			if(x && x.indexOf("ssm")==0){
				P7_SSMtrig(x.substring(3));
			}
		}
	}
}
function P7_SSMtrig(n){
	var i,td,pp,tA,td,tP;
	if(!n){
		n=p7sscur;
		n++;
	}
	else{
		n--;
	}
	if(n>=p7sstmb.length || n<0){
		n=0;
	}
	tA=p7sstmb[n];
	pp=tA.parentNode;
	while(pp){
		if(pp.nodeName=='DIV' && pp.id && pp.id.indexOf('p7SSMwp'==0)){
			td=pp.id.replace('w','t');
			tP=document.getElementById(td);
			break;
		}
		pp=pp.parentNode;
	}
	if(tP){
		if(tP.p7state!='open'){
			P7_SSMopenPanel(tP.id);
		}
	}
	P7_SSMtmb(tA,1);
}
function P7_SSMclear(){
	if(p7ssmtimer){
		clearTimeout(p7ssmtimer);
	}
}
function P7_SSMppTrig(){
	if(!p7ssmn){
		return false;
	}
	var a=document.getElementById('p7ssm_pp');
	if(a.p7state=='play'){
		P7_SSMpause();
	}
	else{
		P7_SSMplay();
	}
	return false;
}
function P7_SSMplay(bp){
	var b=document.getElementById('p7ssm_pp');
	if(p7ssmmode=='auto'&&bp!=1){
		return;
	}
	b.p7state='play';
	p7ssmmode='auto';
	b.title='Pause Slideshow';
	b.className='p7ssm_pause';
	if(bp!=1){
		P7_SSMtrig();
	}
}
function P7_SSMpause(bp){
	var b=document.getElementById('p7ssm_pp');
	P7_SSMclear();
	b.p7state='pause';
	if(bp!=1){
		p7ssmmode='pause';
	}
	b.title='Play Slideshow';
	b.className='p7ssm_play';
}
function P7_SSMctrl(ag,bp){
	if(!p7ssmn){
		return false;
	}
	var n;
	if(ag=='pause'){
		P7_SSMpause();
		return false;
	}
	if(p7ssmbusy){
		return false;
	}
	if(ag=='play'){
		P7_SSMplay();
		return false;
	}
	if(bp!=1){
		P7_SSMpause();
	}
	if(ag=='first'){
		n=1;
	}
	else if(ag=='last'){
		n=p7sstmb.length;
	}
	else if(ag=='next'){
		n=p7sscur+1;
		n++;
		if(n>p7sstmb.length){
			n=1;
		}
	}
	else if(ag=='previous'){
		n=p7sscur+1;
		n--;
		if(n<1){
			n=p7sstmb.length;
		}
	}
	else{
		n=parseInt(ag);
	}
	if(n<1){
		n=1;
	}
	if(n>p7sstmb.length){
		n=p7sstmb.length;
	}
	P7_SSMclear();
	P7_SSMtrig(n);
	return false;
}
function P7_SSMshowImage(n){
	var x,i,tA,tI,pV,iV,pI,fsw,cI,lD;
	x=n-1;
	if(n>p7sstmb.length || n<1 || x==p7sscur){
		return;
	}
	p7ssmbusy=true;
	var cf=document.getElementById("p7ssm_cf");
	cf.innerHTML='';
	cf.style.left="0px";
	cf.style.top="0px";
	if(p7sstmb[p7sscur]){
		p7sstmb[p7sscur].className='';
	}
	tA=p7sstmb[x];
	tA.className="down";
	p7sscur=x;
	tI=P7_getChildIm(tA);
	pV=document.getElementById("p7ssm_preview");
	iV=P7_getChildIm(pV);
	if(!iV.busy){
		iV.src=tI.src;
		iV.alt=tI.alt;
	}
	pI=document.p7sspre[x];
	if(document.p7ssmwait){
		clearTimeout(document.p7ssmwait);
	}
	if(!pI.cmp){
		lD=document.getElementById("p7ssm_loading");
		P7_SSMshowPRG();
		pI.src=tA.href;
		P7_SSMwait(x);
	}
	else{
		P7_SSMdisplay();
	}
}
function P7_SSMdisplay(){
	var i,x,n,op,fsw,fsc,fsimg,fslnk,ch,cw,gld=false,tC,tA,tI,tD,hr,alT,fd,a,cIM,nIM,oiw,oih,niw,nih,nw,nh,fDS,cf,nrw,cDim;
	op=(p7ssmfrst)?0:P7ssma[3];
	p7ssgld=false;
	x=p7sscur;
	fsw=document.getElementById("p7ssm_fsw");
	fsc=document.getElementById("p7ssm_fsc");
	cf=document.getElementById("p7ssm_cf");
	cf.style.visibility="hidden";
	cf.innerHTML='<im'+'g sr'+'c="'+document.p7sspre[x].src+'">';
	if(fsc.p7hssz){
		clearTimeout(fsc.p7hssz);
	}
	fsw.style.overflow="visible";
	fsc.style.height="auto";
	ch=fsw.offsetHeight;
	cw=fsw.offsetWidth;
	fsc.style.width=cw+"px";
	tC=document.getElementById("p7ssm_counter");
	n=p7sstmb.length;
	if(tC){
		tC.innerHTML='Image '+(x+1)+' of '+n;
	}
	tA=p7sstmb[x];
	tI=P7_getChildIm(tA);
	tD=tA.parentNode.getElementsByTagName("DIV");
	hr=null;
	p7ssmAlt=tI.alt;
	fd=null;
	if(tD){
		for(i=0;i<tD.length;i++){
			if(tD[i].className=="p7ssm_lk"){
				a=tD[i].getElementsByTagName("A")[0];
				if(a){
					hr=a.href;
					p7ssmAlt=a.innerHTML;
				}
			}
			if(tD[i].className=="p7ssm_fd"){
				fd=tD[i].innerHTML;
			}
		}
	}
	fDS=document.getElementById("p7ssm_description");
	fDS.p7desc=fd;
	fslnk=document.getElementById("p7ssm_fslink");
	if(hr){
		fslnk.setAttribute("href",hr);
	}
	else{
		fslnk.removeAttribute("href");
	}
	nih=cf.offsetHeight;
	niw=cf.offsetWidth;
	fsimg=document.getElementById("p7ssm_fsimg");
	cIM=document.getElementById("p7ssm_im");
	oiw=cIM.offsetWidth;
	oih=cIM.offsetHeight;
	nih=(nih>20)?nih:oih;
	niw=(niw>20)?niw:oiw;
	nw=oiw-niw;
	nh=oih-nih;
	fsc.p7cw=cw;
	fsc.p7ch=ch;
	nrw=cw-oiw+niw;
	fsc.p7nw=nrw;
	if(op==1){
		if(Math.abs(nw)>0 || Math.abs(nh)>0){
			p7ssgld=true;
			fsc.style.overflow="hidden";
			fsc.style.height=ch+"px";
			fsc.style.width=cw+"px";
		}
	}
	if(p7ssgld){
		P7_SSMfadeOut("p7ssm_im",.95);
	}
	else{
		fsc.style.width=nrw+"px";
		if(fDS.p7desc){
			fDS.innerHTML=fDS.p7desc;
			fDS.style.display="block";
		}
		else{
			fDS.innerHTML='';
			fDS.style.display="none";
		}
		if(op==1){
			cDim=P7_SSMgetPos(cIM);
			P7_SSMsetPos('p7ssm_cf',cDim[1],cDim[0]);
			P7_SSMcrossFade('p7ssm_cf',.05,'p7ssm_im',.95);
		}
		else{
			cIM.src=document.p7sspre[x].src;
			cf.innerHTML='';
			if(nih<1 || niw<1){
				cIM.height=null;
				cIM.width=null;
			}
			else{
				cIM.setAttribute("width",niw);
				cIM.setAttribute("height",nih);
			}
			cIM.setAttribute("alt",p7ssmAlt);
			P7_SSMdispFin();
		}
	}
}
function P7_SSMdispFin(){
	var x,pI;
	p7ssmfrst=false;
	p7ssmbusy=false;
	if(p7ssmmode=='auto'){
		x=p7sscur;
		x++;
		if(x<0||x>=p7sstmb.length){
			x=0;
		}
		if(P7ssma[7]==1){
			pI=document.p7sspre[x];
			if(!pI.cmp){
				pI.src=p7sstmb[x].href;
			}
		}
		p7ssmtimer=setTimeout("P7_SSMtrig()",P7ssma[8]*1000);
	}
}
function P7_SSMdisplayCf(){
	var x=p7sscur;
	var cIM=document.getElementById("p7ssm_im");
	var cf=document.getElementById("p7ssm_cf");
	cIM.src=document.p7sspre[x].src;
	cIM.setAttribute("width",cf.offsetWidth);
	cIM.setAttribute("height",cf.offsetHeight);
	cIM.setAttribute("alt",p7ssmAlt);
	if((navigator.appVersion.indexOf("MSIE")>-1)){
		cIM.style.filter='alpha(opacity=100)';
	}
	else{
		cIM.style.opacity=0.99;
	}
	var cf=document.getElementById("p7ssm_cf");
	cf.style.visibility="hidden";
	cf.innerHTML='';
	P7_SSMdispFin();
}
function P7_SSMdisplayGa(){
	var fsw,fsc,fDS,cIM,nh,cf;
	fsc=document.getElementById("p7ssm_fsc");
	fsw=document.getElementById("p7ssm_fsw");
	cf=document.getElementById("p7ssm_cf");
	fDS=document.getElementById("p7ssm_description");
	fsw.style.visibility="hidden";
	fsw.style.width=fsc.p7nw+"px";
	if(fDS.p7desc){
		fDS.innerHTML=fDS.p7desc;
		fDS.style.display="block";
	}
	else{
		fDS.innerHTML='';
		fDS.style.display="none";
	}
	cIM=document.getElementById("p7ssm_im");
	cIM.src=document.p7sspre[p7sscur].src;
	cIM.setAttribute("width",cf.offsetWidth);
	cIM.setAttribute("height",cf.offsetHeight);
	cf.innerHTML='';
	cIM.setAttribute("alt",p7ssmAlt);
	nh=fsw.offsetHeight;
	fsw.style.display="none";
	P7_SSMszg("p7ssm_fsc","p7ssm_fsw",fsc.p7ch,fsc.p7cw,nh,fsc.p7nw);
}
function P7_SSMdisplayGb(){
	P7_SSMdispFin();
}
function P7_SSMwait(x){
	var im=document.p7sspre[x];
	if(im.cmp || (!document.all&&im.height>1)){
		im.cmp=true;
		document.getElementById("p7ssm_loading").style.visibility="hidden";
		P7_SSMdisplay();
	}
	else{
		if(document.p7ssmwait){
			clearTimeout(document.p7ssmwait);
		}
		document.p7ssmwait=setTimeout("P7_SSMwait("+x+")",60);
	}
}
function P7_SSMshowPRG(g){
	var i,im,tD,tB,ph,pw,ih,iw,nh,nw,lf,tp,cp;
	tD=document.getElementById("p7ssm_loading");
	tB=document.getElementById("p7ssm_fsimg");
	im=P7_getChildIm(tB);
	if(p7ssmfrst){
		im=document.getElementById("p7ssm");
	}
	if(im){
		ph=tD.offsetHeight;
		pw=tD.offsetWidth;
		ih=im.offsetHeight;
		iw=im.offsetWidth;
		lf=parseInt((iw-pw)/2);
		tp=parseInt((ih-ph)/2);
		cp=P7_SSMgetPos(im);
		lf+=cp[0];
		tp+=cp[1];
		P7_SSMsetPos(tD.id,tp,lf);
		tD.style.visibility="visible";
	}
}
function P7_SSMszg(id,id2,ch,cw,th,tw){
	var i,d,dd,mh,mw,ih=5,iw=5,dy=20;
	d=document.getElementById(id);
	dd=document.getElementById(id2);
	d.style.height=ch+'px';
	d.style.width=cw+'px';
	mh=(ch<=th)?0:1;
	mw=(cw<=tw)?0:1;
	ih=(mh==1)?ih*-1:ih;
	iw=(mw==1)?iw*-1:iw;
	if(ch==th && cw==tw){
		dd.style.display="block";
		d.style.overflow="visible";
		d.style.height=dd.offsetHeight+"px";
		dd.style.visibility="visible";
		setTimeout("P7_SSMfadeIn('p7ssm_im',.05)",250);
	}
	else{
		ch+=ih;
		cw+=iw;
		if(mh==0){
			ch=(ch>=th)?th:ch;
		}
		else{
			ch=(ch<=th)?th:ch;
		}
		if(mw==0){
			cw=(cw>=tw)?tw:cw;
		}
		else{
			cw=(cw<=tw)?tw:cw;
		}
		if(d.p7hssz){
			clearTimeout(d.p7hssz);
		}
		d.p7hssz=setTimeout("P7_SSMszg('"+id+"','"+id2+"',"+ch+","+cw+","+th+','+tw+")",dy);
	}
}
function P7_SSMfadeIn(id,op){
	var im,dy,lm=.99,inc,d=document.getElementById(id);
	inc=(window.opera)?.15:.10;
	dy=(window.opera)?20:20;
	if(d.style.visibility!="visible"){
		d.style.visibility="visible";
	}
	op=(op>=lm)?lm:op;
	if((navigator.appVersion.indexOf("MSIE")>-1)){
		d.style.filter='alpha(opacity='+op*100+')';
	}
	else{
		d.style.opacity=op;
	}
	if(op<lm){
		if(d.p7ssmfade){
			clearTimeout(d.p7ssmfade);
		}
		op+=inc;
		d.p7ssmfade=setTimeout("P7_SSMfadeIn('"+id+"',"+op+")",dy);
	}
	else{
		P7_SSMdisplayGb();
	}
}
function P7_SSMfadeOut(id,op){
	var im,dy,inc,cf,lm=.01,d=document.getElementById(id);
	inc=(window.opera)?.12:.10;
	dy=(window.opera)?20:20;
	if(d.style.visibility!="visible"){
		d.style.visibility="visible";
	}
	op=(op<=lm)?lm:op;
	if((navigator.appVersion.indexOf("MSIE")>-1)){
		d.style.filter='alpha(opacity='+op*100+')';
	}
	else{
		d.style.opacity=op;
	}
	if(op>lm){
		if(d.p7ssmfade){
			clearTimeout(d.p7ssmfade);
		}
		op-=inc;
		d.p7ssmfade=setTimeout("P7_SSMfadeOut('"+id+"',"+op+")",dy);
	}
	else{
		d.style.visibility="hidden";
		setTimeout("P7_SSMdisplayGa()",150);
	}
}
function P7_SSMcrossFade(di,opi,dou,opo){
	var dIn,dOut,inc,dy,lm=01,hl=.99;
	inc=.05;
	dy=20;
	dIn=document.getElementById(di);
	dOut=document.getElementById(dou);
	if(dIn.style.visibility!="visible"){
		dIn.style.visibility="visible";
	}
	if(dOut.style.visibility!="visible"){
		dOut.style.visibility="visible";
	}
	opi=(opi>=hl)?hl:opi;
	opo=(opo<=lm)?lm:opo;
	if((navigator.appVersion.indexOf("MSIE")>-1)){
		dIn.style.filter='alpha(opacity='+opi*100+')';
		dOut.style.filter='alpha(opacity='+opo*100+')';
	}
	else{
		dIn.style.opacity=opi;
		dOut.style.opacity=opo;
	}
	if(opo>lm || opi<hl){
		if(dIn.p7ssmfade){
			clearTimeout(dIn.p7ssmfade);
		}
		opi+=inc;
		opo-=inc;
		dIn.p7ssmfade=setTimeout("P7_SSMcrossFade('"+di+"',"+opi+",'"+dou+"',"+opo+")",dy);
	}
	else{
		P7_SSMdisplayCf();
	}
}
function P7_SSMtmb(a,bp){
	var m=(bp==1)?true:false;
	if(!p7ssmn){
		return m;
	}
	if(bp!=1 && p7ssmbusy){
		return m;
	}
	if(bp!=1){
		P7_SSMpause();
	}
	P7_SSMshowImage(a.p7ssindex+1);
	return m;
}
function P7_SSMtmb_ovr(a){
	if(!p7ssmn){
		return;
	}
	var iM,pV,iV,cl,ph,pw,of,newTop,newLeft,wW,scl,tw;
	iM=P7_getChildIm(a);
	pV=document.getElementById("p7ssm_preview");
	iV=P7_getChildIm(pV);
	iV.busy=true;
	iV.src=iM.src;
	iV.alt=iM.alt;
}
function P7_SSMtmb_out(a){
	if(!p7ssmn){
		return;
	}
	var pI,tI,pV=document.getElementById("p7ssm_preview");
	pI=P7_getChildIm(pV);
	tI=P7_getChildIm(p7sstmb[p7sscur]);
	pI.busy=false;
	pI.src=tI.src;
	pI.alt=tI.alt;
}
function P7_SSMtrgPanel(d,c){
	if(!p7ssmn){
		return false;
	}
	var i,a,x,td;
	if(c==1){
		P7_SSMpause();
	}
	a=document.getElementById(d);
	if(a){
		if(a.p7state=="open"){
			P7_SSMclosePanel(d,c);
		}
		else{
			P7_SSMopenPanel(d,c);
		}
	}
	return false;
}
function P7_SSMtoggle(id,c){
	var i,x,pd,nd;
	for(i=0;i<P7ssmpnls.length;i++){
		pd=P7ssmpnls[i];
		x=pd.indexOf("_");
		nd=pd.substring(0,x+1);
		if(x>0&&pd.indexOf(nd)==0&&pd!=id){
			P7_SSMclosePanel(pd,c);
		}
	}
}
function P7_SSMclosePanel(iD,c){
	var i,a,wD,tW,tC,h;
	a=document.getElementById(iD);
	if(a){
		if(a.p7state=="closed"){
			return;
		}
		if(c==1){
			P7_SSMpause();
		}
		wD=iD.replace('t','w');
		tW=document.getElementById(wD);
		if(tW){
			tC=tW.getElementsByTagName("DIV")[0];
			if(tC){
				a.p7state="closed";
				a.className='';
				if(P7ssma[1]==1){
					if(P7_hasOverflow(tW)||P7_hasOverflow(tC)){
						tC.style.display="block";
						return;
					}
					h=tW.offsetHeight;
					tW.style.overflow="hidden";
					P7_SSMglide(tW.id,h,0);
				}
				else{
					tC.style.display="none";
				}
			}
		}
	}
}
function P7_SSMopenPanel(iD,c){
	var i,a,wD,tW,tC,h;
	a=document.getElementById(iD);
	if(a){
		if(a.p7state=="open"){
			return;
		}
		if(c==1){
			P7_SSMpause();
		}
		wD=iD.replace('t','w');
		tW=document.getElementById(wD);
		if(tW){
			tC=tW.getElementsByTagName("DIV")[0];
			if(tC){
				a.p7state="open";
				a.className="down";
				if(P7ssma[1]==1){
					if(P7_hasOverflow(tW)||P7_hasOverflow(tC)){
						tC.style.display="block";
						return;
					}
					tW.style.overflow="hidden";
					tW.style.height="1px";
					tC.style.display="block";
					h=tC.offsetHeight;
					if(h>0){
						P7_SSMglide(tW.id,1,h);
					}
					else{
						tW.style.height="auto";
						tC.style.display="block";
					}
				}
				else{
					tC.style.display="block";
				}
				if(a.p7type=='toggle'){
					if(P7ssma[2]==1){
						P7_SSMtoggle(iD,c);
					}
				}
			}
		}
	}
}
function P7_SSMglide(dd,ch,th){
	var w,m,d,tC,tt,dy=10,inc=10,pc=.15;
	d=document.getElementById(dd);
	m=(ch<=th)?0:1;
	tt=Math.abs( parseInt( Math.abs(th)-Math.abs(ch) ) );
	inc=(tt*pc<1)?1:tt*pc;
	inc=(m==1)?inc*-1:inc;
	d.style.height=ch+"px";
	if(ch==th){
		if(th==0){
			tC=d.getElementsByTagName('DIV')[0];
			tC.style.display="none";
			d.style.height="auto";
		}
		else{
			d.style.height="auto";
		}
	}
	else{
		ch+=inc;
		if(m==0){
			ch=(ch>=th)?th:ch;
		}
		else{
			ch=(ch<=th)?th:ch;
		}
		if(d.p7ssG){
			clearTimeout(d.p7ssG);
		}
		d.p7ssG=setTimeout("P7_SSMglide('"+dd+"',"+ch+","+th+")",dy);
	}
}
function P7_SSMeng(evt){
	if(!p7ssmn){
		return false;
	}
	var x,mx,tx,y,my,ty;
	evt=(evt)?evt:event;
	var m=true,tS,tg=(evt.target)?evt.target:evt.srcElement;
	p7ssmsobj=null;
	if(tg.id&&tg.id=='p7ssm_slidebar'){
		p7ssmsobj=document.getElementById('p7ssm_slider');
	}
	else if(tg.id&&tg.id=='p7ssm_dragbar'){
		p7ssmsobj=document.getElementById('p7SSMwhmb');
	}
	if(p7ssmsobj){
		if(evt.clientX){
			x=(p7ssmsobj.offsetLeft)?p7ssmsobj.offsetLeft:0;
			y=(p7ssmsobj.offsetTop)?p7ssmsobj.offsetTop:0;
			mx=parseInt(P7_getPropValue(p7ssmsobj,'marginLeft','margin-left'));
			mx=(mx)?mx:0;
			my=parseInt(P7_getPropValue(p7ssmsobj,'marginTop','margin-top'));
			my=(my)?my:0;
			p7ofX=evt.clientX-(x-mx);
			p7ofY=evt.clientY-(y-my);
		}
		m=false;
		if(tg.id=='p7ssm_slidebar'){
			document.getElementById('p7ssm_slidebar').title='';
			tS=document.getElementById('p7ssm_speed');
			if(tS){
				tS.style.display='block';
			}
			p7ssmspd=P7ssma[8];
			if(p7ssmmode=='auto'){
				P7_SSMpause();
			}
		}
	}
	return m;
}
function P7_SSMdrg(evt){
	evt=(evt)?evt:event;
	var m=true;
	if(p7ssmsobj){
		if(evt.clientX){
			if(p7ssmsobj.id=='p7ssm_slider'){
				P7_SSMshift(p7ssmsobj,(evt.clientX-p7ofX));
			}
			else{
				P7_SSMshiftThumb(p7ssmsobj,(evt.clientX-p7ofX),(evt.clientY-p7ofY));
			}
		}
		evt.cancelBubble=true;
		m=false;
	}
	return m;
}
function P7_SSMshiftThumb(obj,x,y){
	obj.style.left=x+"px";
	obj.style.top=y+"px";
}
function P7_SSMshift(obj,x){
	var r,sr,sp,tS,tC,tT;
	tS=document.getElementById('p7ssh_speed');
	if(tS){
		tS.style.display='block';
	}
	tC=document.getElementById('p7ssm_slidechannel');
	r=tC.offsetWidth;
	x=(x<=0)?0:x;
	x=(x>=r)?r:x;
	obj.style.left=x+"px";
	sp = Math.round( (x/r) * p7ssmMaxSpeed );
	if(sp<=p7ssmMinSpeed){
		sp=p7ssmMinSpeed;
	}
	tT=document.getElementById('p7ssm_speed');
	if(tT){
		tT.innerHTML='Delay:&nbsp;'+sp+'&nbsp;seconds';
	}
	p7ssmspd=sp;
}
function P7_SSMrel(evt){
	var tS;
	if(p7ssmsobj){
		if(p7ssmsobj.id=='p7ssm_slider'){
			tS=document.getElementById('p7ssm_speed');
			if(tS){
				tS.style.display='none';
			}
			P7ssma[8]=p7ssmspd;
			document.getElementById('p7ssm_slidebar').title='Delay: '+P7ssma[8]+' seconds';
			if(p7ssmmode=='pause'){
				P7_SSMplay();
			}
		}
		p7ssmsobj=null;
	}
}
function P7_SSMsetDelay(s){
	var rr,sp,x,r,tC,sD,tT;
	tC=document.getElementById('p7ssm_slidechannel');
	sD=document.getElementById('p7ssm_slider');
	r=tC.offsetWidth;
	if(!s){
		s=P7ssma[8];
	}
	s=(s<=p7ssmMinSpeed)?p7ssmMinSpeed:s;
	s=(s>=p7ssmMaxSpeed)?p7ssmMaxSpeed:s;
	x=Math.round( (s / p7ssmMaxSpeed) *r );
	sD.style.left=x+'px';
	P7ssma[8]=s;
	tT=document.getElementById('p7ssm_speed');
	if(tT){
		tT.innerHTML='Delay:&nbsp;'+sp+'&nbsp;seconds';
	}
	document.getElementById('p7ssm_slidebar').title='Delay: '+P7ssma[8]+' seconds';
}
function P7_getChildIm(d){
	return d.getElementsByTagName('IMG')[0];
}
function P7_hasOverflow(ob){
	var s,m;
	s=ob.style.overflow;
	if(!s){
		s=P7_getPropValue(ob,'overflow','overflow');
	}
	m=(s&&s=='auto')?true:false;
	return m;
}
function P7_getPropValue(ob,prop,prop2){
	var h,v=null;
	if(ob){
		if(ob.currentStyle){
			v=eval('ob.currentStyle.'+prop);
		}
		else if(document.defaultView.getComputedStyle(ob,"")){
			v=document.defaultView.getComputedStyle(ob,"").getPropertyValue(prop2);
		}
		else{
			v=eval("ob.style."+prop);
		}
	}
	return v;
}
function P7_SSMgetPos(obj){
	var t=0,l=0,pp,pos=new Array(),ct,cl,bt=0,bl=0;
	var isFF=navigator.userAgent.indexOf("Firefox")>-1;
	pp=obj;
	var ct,di;
	while(pp){
		l+=(pp.offsetLeft)?pp.offsetLeft:0;
		t+=(pp.offsetTop)?pp.offsetTop:0;
		if(isFF){
			pp.p7pn=true;
		}
		pp=pp.offsetParent;
	}
	if(isFF){
		pp=obj.parentNode;
		while(pp){
			if(!pp.p7pn&&pp.clientHeight){
				ct=(pp.offsetHeight-pp.clientHeight)/2;
				bt=(ct)?bt+ct:bt;
				cl=(pp.offsetWidth-pp.clientWidth)/2;
				bl=(cl)?bl+cl:bl;
			}
			if(pp.nodeName=="BODY"){
				break;
			}
			pp=pp.parentNode;
		}
		if(cl>0 || bl>0){
		}
	}
	pos[0]=l;
	pos[1]=t;
	return pos;
}
function P7_SSMsetPos(d,t,l){
	var i,pp,p,pf,tD=document.getElementById(d);
	pp=tD.parentNode;
	while(pp){
		if(pp.style){
			p=P7_getPropValue(pp,'position','position');
			if(p=="absolute" || p=="relative"){
				pf=P7_SSMgetPos(pp);
				l-=pf[0];
				t-=pf[1];
				break;
			}
		}
		pp=pp.parentNode;
	}
	tD.style.top=t+"px";
	tD.style.left=l+"px";
}
function P7_imgClick(a){
	if(a.href && a.href.indexOf(".")>-1){
		P7_SSMpause();
		document.p7NW=window.open(a.href,'ImageLink');
		document.p7NW.focus();
	}
	return false;
}
if(window.addEventListener){
	window.addEventListener("unload",P7_FFfix,false);
}
function P7_FFfix(){
	return;
}
