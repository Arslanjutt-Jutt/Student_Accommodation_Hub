function addHoverEffect(hltargetId, parentSelector, childSelector) {

   

    

    // Add hover functionality
    $(hltargetId).hover(
        function () {
            // Show the child element when hovering over the parent
            
            $(childSelector)                ;
        },
        function () {
            // Hide the child element when the cursor leaves the parent
            $(childSelector).hide();
        }
    );
}
function closePopup() {
    // Close the ModalPopupExtender using JavaScript
    var popup = $find('<%= mpePopup.ClientID %>');
    popup.hide();
    return false;
}
//table.LISmenuLinks.subMenuOptionsPanel
//{
//    background: #DDDDDD url(../../images / gradient.png) repeat - x 0 - 50px;
//    padding: 0px;
//    line - height: .5em; text - align: left;
//    border: solid 1px #b4b4b4; /border-radius: 1em; -webkit-border-radius: 1em; -moz-border-radius: 1em;/
//        - webkit - box - shadow: 0 1px 3px rgba(0, 0, 0, .4); -moz - box - shadow: 0 1px 3px rgba(0, 0, 0, .4); z - index: 99999;
//}
//function ZE_ShowHideHoverPanel(hoverPanelId, showIt) {
//    if (showIt) { $('#' + hoverPanelId).css('display', 'block'); }
//    else { $('#' + hoverPanelId).css('display', 'none'); }
//} function ZE_BindZEHover(showTargetId, hideTargetId, hoverPanelId) { $('#' + hideTargetId).css('display', 'inline-block'); $('#' + hoverPanelId).css('position', 'absolute'); $('#' + hoverPanelId).css('display', 'none'); $('#' + showTargetId).mouseenter(function (e) { ZE_ShowHideHoverPanel(hoverPanelId, true); }); $('#' + hideTargetId).mouseleave(function () { ZE_ShowHideHoverPanel(hoverPanelId, false); }); }
//table.LISmenuLinks.subMenuOptionsPanel a { color: #000000!important; padding: 10px 20px; width: auto;
//height: auto; /border-radius: 9px; -webkit-border-radius: 9px; -moz-border-radius: 9px;/ }
//table.LISmenuLinks.subMenuOptionsPanel a:hover { background - color: #0078FF; color: #FFFFFF!important; }