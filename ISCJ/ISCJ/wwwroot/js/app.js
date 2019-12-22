(function(document, env)
{
  var _env = env;

var ISCJClient = function(document)
{
  var version = "1.0";
	var GetVersionInternal = function()
	{
	  return version;
	}
	
	var process = function(task)
	{
		alert("Process task " + task.Id + "Version # " + version);
	};
	
ISCJClient.prototype.GetService= function(svcName)
{
  alert('execute called on MyFramework version number: ' + this.GetVersion());
};

 ISCJClient.prototype.DisplayVersion = function () {
    alert('execute called on MyFramework version number: ' + GetVersionInternal());
};

};

  document.ISCJClient = new ISCJClient(document);
}(document, "dev"));


