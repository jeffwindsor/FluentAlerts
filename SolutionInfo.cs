using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("www.jeffreywindsor.com")]
[assembly: AssemblyCopyright("Copyright Jeff Windsor 2010-2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

//****************************************
//Semantic Versioning Specification (SemVer)
//****************************************
//Software using Semantic Versioning MUST declare a public API. This API could be declared in the code itself or exist strictly in documentation. However it is done, it should be precise and comprehensive.
//A normal version number MUST take the form X.Y.Z where X, Y, and Z are non-negative integers. X is the major version, Y is the minor version, and Z is the patch version. Each element MUST increase numerically by increments of one. For instance: 1.9.0 -> 1.10.0 -> 1.11.0.
//Once a versioned package has been released, the contents of that version MUST NOT be modified. Any modifications MUST be released as a new version.
//Major version zero (0.y.z) is for initial development. Anything may change at any time. The public API should not be considered stable.
//Version 1.0.0 defines the public API. The way in which the version number is incremented after this release is dependent on this public API and how it changes.
//Patch version Z (x.y.Z | x > 0) MUST be incremented if only backwards compatible bug fixes are introduced. A bug fix is defined as an internal change that fixes incorrect behavior.
//Minor version Y (x.Y.z | x > 0) MUST be incremented if new, backwards compatible functionality is introduced to the public API. It MUST be incremented if any public API functionality is marked as deprecated. It MAY be incremented if substantial new functionality or improvements are introduced within the private code. It MAY include patch level changes. Patch version MUST be reset to 0 when minor version is incremented.
//Major version X (X.y.z | X > 0) MUST be incremented if any backwards incompatible changes are introduced to the public API. It MAY include minor and patch level changes. Patch and minor version MUST be reset to 0 when major version is incremented.
//A pre-release version MAY be denoted by appending a hyphen and a series of dot separated identifiers immediately following the patch version. Identifiers MUST comprise only ASCII alphanumerics and hyphen [0-9A-Za-z-]. Pre-release versions satisfy but have a lower precedence than the associated normal version. Examples: 1.0.0-alpha, 1.0.0-alpha.1, 1.0.0-0.3.7, 1.0.0-x.7.z.92.
//Build metadata MAY be denoted by appending a plus sign and a series of dot separated identifiers immediately following the patch or pre-release version. Identifiers MUST comprise only ASCII alphanumerics and hyphen [0-9A-Za-z-]. Build metadata SHOULD be ignored when determining version precedence. Thus two packages with the same version, but different build metadata are considered to be the same version. Examples: 1.0.0-alpha+001, 1.0.0+20130313144700, 1.0.0-beta+exp.sha.5114f85.
//Precedence MUST be calculated by separating the version into major, minor, patch and pre-release identifiers in that order (Build metadata does not figure into precedence). Major, minor, and patch versions are always compared numerically. Pre-release precedence MUST be determined by comparing each dot separated identifier as follows: identifiers consisting of only digits are compared numerically and identifiers with letters or hyphens are compared lexically in ASCII sort order. Numeric identifiers always have lower precedence than non-numeric identifiers. Example: 1.0.0-alpha < 1.0.0-alpha.1 < 1.0.0-beta.2 < 1.0.0-beta.11 < 1.0.0-rc.1 < 1.0.0.
//  From : http://semver.org
[assembly: AssemblyVersion("0.9.0")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

