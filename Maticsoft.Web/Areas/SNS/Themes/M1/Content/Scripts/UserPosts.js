$(function () {
    if ($.getUrlParam("type") == "user") {
        $('#UserAllBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#UserPhotoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserProductBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserVideoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserBlogBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "photo") {
        $('#UserAllBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserPhotoBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#UserProductBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserVideoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserBlogBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "product") {
        $('#UserPhotoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserProductBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#UserAllBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserVideoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserBlogBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "video") {
        $('#UserPhotoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserVideoBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#UserProductBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserAllBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserBlogBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
    if ($.getUrlParam("type") == "blog") {
        $('#UserBlogBtn').removeClass('tmenu_b').addClass('tmenu_a');
        $('#UserPhotoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserVideoBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserProductBtn').removeClass('tmenu_a').addClass('tmenu_b');
        $('#UserAllBtn').removeClass('tmenu_a').addClass('tmenu_b');
    }
});
