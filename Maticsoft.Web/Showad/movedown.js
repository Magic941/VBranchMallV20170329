/* File Created: 六月 6, 2012 */
var speed = 30
var colee_bottom2 = document.getElementById("colee2");
var colee_bottom1 = document.getElementById("colee1");
var colee_bottom = document.getElementById("colee");
colee_bottom2.innerHTML = colee_bottom1.innerHTML
colee_bottom.scrollTop = colee_bottom.scrollHeight
function Marquee2() {
    if (colee_bottom1.offsetTop - colee_bottom.scrollTop >= 0)
        colee_bottom.scrollTop += colee_bottom2.offsetHeight
    else {
        colee_bottom.scrollTop--
    }
}
var MyMar2 = setInterval(Marquee2, speed)
colee_bottom.onmouseover = function () { clearInterval(MyMar2) }
colee_bottom.onmouseout = function () { MyMar2 = setInterval(Marquee2, speed) }