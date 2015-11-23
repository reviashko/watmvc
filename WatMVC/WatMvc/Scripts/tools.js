var baseUrl = "http://www.watshop.ru/";
function InitLeftMenu(){ /*$('#treeMenu ul li div ul li:has("div")').find('span:first').addClass('closed');$('#treeMenu ul li div ul li:has("div")').find('div').hide();*/ $('#treeMenu li:has("div")').find('span:first').click(function (){$(this).parent('li').find('span:first').toggleClass('opened');$(this).parent('li').find('a:first').toggleClass('openedA');$(this).parent('li').find('div:first').slideToggle();});}
function ShowShortInfo(cod,sum,name,top,left){setTimeout(function(){if($("#"+cod).attr("ref") == "1"){$(".addDialogBox").css("display","block").css("top",top).css("left",left);$(".adbi_img").attr("src","/gimg/small/"+cod+".jpg");$(".adbi_sum").html(sum);$(".adbi_name").html(name);$("#adbi_ab").attr("rel",cod);$("#AddedMsg").css("display","none");}},500);}
function InitGoodsHolder() { $(".addDialogBox").css("display", "none"); $("#adbi_cb").click(function () { $(".addDialogBox").css("display", "none"); return false; }); $("#adbi_ab").click(function () { AddGoodsToBasket($(this).attr("rel"), 0, $("[id=quantity]").val(), parseInt($('.adbi_sum').text())); return false; }); $(".catItemOrder").mouseover(function () { $(this).parent().attr("ref", "1"); ShowShortInfo($(this).parent().attr("id"), $(this).parent().attr("rel"), $(this).parent().attr("alt"), $(this).offset().top - 195, $(this).offset().left - 110); }); $(".catItemOrder").mouseout(function () { $(this).parent().attr("ref", "0"); }); $(".addDialogBox").mouseleave(function () { $(this).css("display", "none"); }); }
function glxerror(result) { alert(result.status + ' ' + result.statusText); }
function AddGoodsToBasket(price_id,mech_price_id,quantity,price){$.ajax({ type: "POST", async: false, url: "/wildService.asmx/AddBasketItem", data: "{'price_id':" + price_id + ",'mech_price_id':" + mech_price_id + ",'quantity':" + quantity + ",'price':" + price + "}", contentType: "application/json; charset=utf-8", dataType: "json", success: function (msg) { var jo = jQuery.parseJSON(msg.d); ShowAddDialog(jo.ts, jo.tc, jo.rv1, jo.rv2); }, error: glxerror }); return false; }
function ShowAddDialog(tsum,tqnt,retval1,retval2){UpdateBskInfo(tsum,tqnt);$("#AddedMsg").show();$(".BtnWrap2").show();$(".BtnWrap").hide();setTimeout(function(){$(".BtnWrap").show();$(".BtnWrap2").hide();},3500);}
function UpdateBskInfo(tsum,tqnt){$("#bqnt").html(tqnt);$("#bsum").html(tsum);}
function InitFindAutoComplete(ftext_id,fbtn_id){$(ftext_id).available(function(){$(ftext_id).autocomplete({serviceUrl:'/wildService.asmx/GetSearchCompletionList',minChars:2,maxHeight:400,width:430,zIndex:9999,deferRequestBy:500,noCache:false,onSelect:function(value,data){$(fbtn_id).click();}});});}
function InitFindControl(textbox_id,deftext_id){var deftxt=$(deftext_id).val();$(textbox_id).blur(function(){if($(this).val().length==0){$(this).val(deftxt);}}).click(function(){if($(this).val()==deftxt){$(this).val("");}});$(textbox_id).val(deftxt);}
function InitScrollBox(){$("#scrollBox").hide();$(window).scroll(function(){if($(window).scrollTop()>100 && scrollIsHide){$("#scrollBox").show();scrollIsHide=false;}if($(window).scrollTop()<101 && !scrollIsHide){$("#scrollBox").hide();scrollIsHide=true;}});}
function InitBrandMenu(){$.ajax({type:"POST", url: "/wildService.asmx/GetBrandsMenu", data: "{}",contentType:"application/json; charset=utf-8",dataType:"json",success:function(msg){$("#brands_holder").html(msg.d);},error:glxerror});return false;}
function AddTableEvents(){$("#GoodsHolderTable tbody tr").mouseover(function(){$(this).css("background-color","#8E8CB7");}).mouseout(function(){$(this).css("background-color","transparent");}).click(function(){parent.location.href = $("#"+$(this).attr("id")+"_url").attr("href");return false;});}
function LightBoxImg(path,tit)
{var obj=document.getElementById('preview_img');if(obj!=null)
{obj.title=tit;obj.href=path;obj.onclick();SetStyle();}}
function SetStyle()
{var obj1=document.getElementById('lightbox-overlay');var obj2=document.getElementById('lightbox');if(obj1!=null&&obj2!=null)
{$("#lightbox-overlay").css({position:"absolute",zIndex:"999"});$("#lightbox").css({position:"absolute",zIndex:"1000"});}}
function GetParamItem(name,content)
{return name+'='+encodeURI(content);}
function DelTableRow(senderObj,tableName)
{var i=senderObj.parentNode.parentNode.rowIndex;document.getElementById(tableName).deleteRow(i);}
function HideForm(id)
{var obj=document.getElementById(id);if(obj!=null)obj.style.display='none';}
function SelectFirstIndex(ddl_id)
{var obj=document.getElementById(ddl_id);if(obj!=null)
{if(obj.options.length>0)obj.selectedIndex=0;}}
function ReadFile(fileUrl,isText){var req;var fileContent;if(window.ActiveXObject){req=new ActiveXObject("Microsoft.XMLHTTP");req.open("GET",fileUrl,false);req.onreadystatechange=function(){if(req.readyState==4&&req.status==200){fileContent=isText?req.responseText:req.responseXml;}}
req.send(null);}else if(window.XMLHttpRequest){req=new XMLHttpRequest();req.open("GET",fileUrl,false);req.send(null);fileContent=isText?req.responseText:req.responseXML;}
return fileContent;}
function AddErrorDiv(id,className,content){if(!isExists(id))
{var new_div=document.createElement('div');new_div.id=id;if(className!=null)new_div.className=className;new_div.innerHTML=content;document.body.appendChild(new_div);}
else
{SetInnerHTML(id,content);}}
function isExists(id)
{var obj=document.getElementById(id);if(obj!=null)
{return true;}
return false;}
function TrimString(sInString)
{sInString=sInString.replace(/ /g,' ');return sInString.replace(/(^\s+)|(\s+$)/g,"");}
function WriteValue(id,value)
{var obj=document.getElementById(id);if(obj!=null)
{obj.value=value;}}
function ReadValue(id)
{var obj=document.getElementById(id);if(obj!=null)
{return TrimString(obj.value);}}
function SetInnerHTML(id,content)
{var obj=document.getElementById(id);if(obj!=null)
{obj.innerHTML=content;}}
function GetInnerHTML(id)
{var obj=document.getElementById(id);if(obj!=null)
{return obj.innerHTML;}}
function SetInnerText(id,content)
{var obj=document.getElementById(id);if(obj!=null)
{document.all?obj.innerText=content:obj.value=content;}}
function GetInnerText(id)
{var obj=document.getElementById(id);if(obj!=null)
{return document.all?obj.innerText:obj.value;}}
function dev_addEvent(elm,evType,fn,useCapture)
{if(elm.addEventListener)
{elm.addEventListener(evType.replace("on",""),fn,useCapture);return true;}
else if(elm.attachEvent)
{var r=elm.attachEvent(evType,fn);return r;}
else
{elm[evType]=fn;}}
function dev_delEvent(elm,evType,fn)
{if(elm.removeEventListener)
{elm.removeEventListener(evType.replace("on",""),fn,true);return true;}
else if(elm.detachEvent)
{var r=elm.detachEvent(evType,fn);return r;}
else
{elm[evType]=null;}}
function bookmarksite(title,url)
{if(document.all)
{window.external.AddFavorite(url,title);}else
if(window.sidebar)
{window.sidebar.addPanel(title,url,"");}}


function addFav(a) {
  title=document.title;
  url=document.location;
  try {
    window.external.AddFavorite(url, title);
  }
  catch(e) {
    try {
      window.sidebar.addPanel(title, url, "");
    }
    catch(e) {
      if (typeof(opera)=="object") {
        a.rel="sidebar";
        a.title=title;
        a.url=url;
        a.href=url;
        return true;
      }
      else {
        alert('Нажмите Ctrl+D, чтобы добавить страницу в закладки.');
      }
    }
  }
  return false;
}