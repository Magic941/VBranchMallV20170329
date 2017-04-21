$(function () {
    var agent_iframe = document.createElement("iframe"),
    b_height = $("body").height();
    agent_iframe.src = "http://static.maticsoft.com/m18shop/agent.html#" + b_height;
    document.body.appendChild(agent_iframe);
    agent_iframe.style.display = "none";
})