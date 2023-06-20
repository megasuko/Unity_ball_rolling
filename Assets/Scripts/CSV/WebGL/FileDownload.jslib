var FileDownLoadPlugin = {
    FileDownLoad: function(str, fileName){
        var blob = new Blob([Pointer_stringify(str)], {type:"text/csv"});
        var link = document.createElement('a');
        link.href = URL.createObjectURL(blob);
        link.download = Pointer_stringify(fileName) + ".csv";
        link.click();
    }
}
mergeInto(LibraryManager.library, FileDownLoadPlugin);