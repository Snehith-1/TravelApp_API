angular.module("textAngular.factories",[]).factory("taBrowserTag",[function(){return function(t){return t?""===t?void 0===_browserDetect.ie?"div":_browserDetect.ie<=8?"P":"p":_browserDetect.ie<=8?t.toUpperCase():t:_browserDetect.ie<=8?"P":"p"}}]).factory("taApplyCustomRenderers",["taCustomRenderers","taDOM",function(t,e){return function(r){var n=angular.element("<div></div>");return n[0].innerHTML=r,angular.forEach(t,function(t){var r=[];t.selector&&""!==t.selector?r=n.find(t.selector):t.customAttribute&&""!==t.customAttribute&&(r=e.getByAttribute(n,t.customAttribute)),angular.forEach(r,function(e){e=angular.element(e),t.selector&&""!==t.selector&&t.customAttribute&&""!==t.customAttribute?void 0!==e.attr(t.customAttribute)&&t.renderLogic(e):t.renderLogic(e)})}),n[0].innerHTML}}]).factory("taFixChrome",function(){var t=function(t){if(!t||!angular.isString(t)||t.length<=0)return t;for(var e,r,n,i=/<([^>\/]+?)style=("([^"]+)"|'([^']+)')([^>]*)>/gi,a="",o=0;e=i.exec(t);)r=e[3]||e[4],r&&r.match(/line-height: 1.[0-9]{3,12};|color: inherit; line-height: 1.1;/i)&&(r=r.replace(/( |)font-family: inherit;|( |)line-height: 1.[0-9]{3,12};|( |)color: inherit;/gi,""),n="<"+e[1].trim(),r.trim().length>0&&(n+=" style="+e[2].substring(0,1)+r+e[2].substring(0,1)),n+=e[5].trim()+">",a+=t.substring(o,e.index)+n,o=e.index+e[0].length);return a+=t.substring(o),o>0?a.replace(/<span\s?>(.*?)<\/span>(<br(\/|)>|)/gi,"$1"):t};return t}).factory("taSanitize",["$sanitize",function(t){function e(t,e){for(var r,n=0,i=0,a=/<[^>]*>/gi;r=a.exec(t);)if(i=r.index,"/"===r[0].substr(1,1)){if(0===n)break;n--}else n++;return e+t.substring(0,i)+angular.element(e)[0].outerHTML.substring(e.length)+t.substring(i)}function r(t){if(!t||!angular.isString(t)||t.length<=0)return t;for(var n,a,o,s,u,g,c=/<([^>\/]+?)style=("([^"]+)"|'([^']+)')([^>]*)>/gi,f="",h="",b=0;a=c.exec(t);){s=a[3]||a[4];var p=new RegExp(l,"i");if(angular.isString(s)&&p.test(s)){u="";for(var v=new RegExp(l,"ig");o=v.exec(s);)for(n=0;n<i.length;n++)o[2*n+2]&&(u+="<"+i[n].tag+">");g=r(t.substring(b,a.index)),h+=f.length>0?e(g,f):g,s=s.replace(new RegExp(l,"ig"),""),h+="<"+a[1].trim(),s.length>0&&(h+=' style="'+s+'"'),h+=a[5]+">",b=a.index+a[0].length,f=u}}return h+=f.length>0?e(t.substring(b),f):t.substring(b)}function n(t){if(!t||!angular.isString(t)||t.length<=0)return t;for(var e,r=/<([^>\/]+?)align=("([^"]+)"|'([^']+)')([^>]*)>/gi,n="",i=0;e=r.exec(t);){n+=t.substring(i,e.index),i=e.index+e[0].length;var a="<"+e[1]+e[5];/style=("([^"]+)"|'([^']+)')/gi.test(a)?a=a.replace(/style=("([^"]+)"|'([^']+)')/i,'style="$2$3 text-align:'+(e[3]||e[4])+';"'):a+=' style="text-align:'+(e[3]||e[4])+';"',a+=">",n+=a}return n+t.substring(i)}for(var i=[{property:"font-weight",values:["bold"],tag:"b"},{property:"font-style",values:["italic"],tag:"i"}],a=[],o=0;o<i.length;o++){for(var s="("+i[o].property+":\\s*(",u=0;u<i[o].values.length;u++)u>0&&(s+="|"),s+=i[o].values[u];s+=");)",a.push(s)}var l="("+a.join("|")+")";return function(e,i,a){if(!a)try{e=r(e)}catch(o){}e=n(e);var s;try{s=t(e),a&&(s=e)}catch(o){s=i||""}var u,l=s.match(/(<pre[^>]*>.*?<\/pre[^>]*>)/gi),g=s.replace(/(&#(9|10);)*/gi,""),c=/<pre[^>]*>.*?<\/pre[^>]*>/gi,f=0,h=0;for(s="";null!==(u=c.exec(g))&&f<l.length;)s+=g.substring(h,u.index)+l[f],h=u.index+u[0].length,f++;return s+g.substring(h)}}]).factory("taToolExecuteAction",["$q","$log",function(t,e){return function(r){void 0!==r&&(this.$editor=function(){return r});var n,i=t.defer(),a=i.promise,o=this.$editor();try{n=this.action(i,o.startAction()),a["finally"](function(){o.endAction.call(o)})}catch(s){e.error(s)}(n||void 0===n)&&i.resolve()}}]);