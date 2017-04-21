/* File Created: 六月 6, 2012 */
//使用div时，请保证colee_left2与colee_left1是在同一行上.
var speed = 30//速度数值越大速度越慢
var colee_left2 = document.getElementById("colee2");
var colee_left1 = document.getElementById("colee1");
var colee_left = document.getElementById("colee");
colee_left2.innerHTML = colee_left1.innerHTML
function Marquee3() {
    if (colee_left2.offsetWidth - colee_left.scrollLeft <= 0)//offsetWidth 是对象的可见宽度
        colee_left.scrollLeft -= colee_left1.offsetWidth//scrollWidth 是对象的实际内容的宽，不包边线宽度
    else {
        colee_left.scrollLeft++
    }
}
var MyMar3 = setInterval(Marquee3, speed)
colee_left.onmouseover = function () { clearInterval(MyMar3) }
colee_left.onmouseout = function () { MyMar3 = setInterval(Marquee3, speed) }