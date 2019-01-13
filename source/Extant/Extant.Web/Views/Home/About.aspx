<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    About
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <div id="content">
	<br />
	<h2>Software</h2>
	<p>The UK-RiME Data Catalogue software was originally developed as part of a collaboration between <a href="http://www.arthritisresearchuk.org/">Arthritis Research UK</a>, 
	<a href="http://research.bmh.manchester.ac.uk/Musculoskeletal/research/CfE/">Arthritis Research UK Centre for Epidemiology</a> and <a href="https://nweh.co.uk/">North West eHealth</a>. Updates to the software have been carried out by Research IT Services at the University of Manchester.
	The software is available for download from the University of Manchester Research IT Services  <a href="https://github.com/UoMResearchIT/extant"> github repository <i class="fa fa-github" style="font-size:24px" color="000000"></i></a> under an open source license.   
	</p>
	<h2> Overview </h2>
        Terms, Conditions and Privacy information for this site can be found <a href="/Terms" title="Terms, Conditions and Privacy Information">here</a>. Please take your time to read these in full. Key points are summarised below:     

        <h3>Privacy</h3>
        <p>The UK-RiME Data Catalogue site complies with the University of Manchester <a href="http://www.manchester.ac.uk/privacy/">privacy policy</a>.</p>
        <p>This privacy policy explains how we will use any personal information about you collected when you register with us or use the catalogue.</p>

        <h4>What information do we collect about you?</h4>
        <p>When you register to use the UK-RiME data catalogue, we collect:</p>
        <ul>
            <li>Your name</li>
            <li>Your email address</li>
            <li>Your affiliated institution</li>
            <li>Your disease area</li>
        </ul>
        <h4>How will we use the information about you?</h4>
        <p>We will use the information which you supply to us to:</p>
        <ul>
            <li>Approve your account e.g. we may contact you by email to confirm your affiliation with one of the UK-RiME centres.</li>
            <li>Provide you with and administer your user account.</li>
            <li>Provide you with email updates about the catalogue service e.g. scheduled down-time.</li>
        </ul>
        <p>The legal basis for processing your information is consent; by registering to use the catalogue, you consent to us using your information for this purpose. The data controller is the University of Manchester.</p>
        <p>We will keep your information for a period of up to one year after your account is closed or the system is removed from service, unless you request that your data be deleted.</p>
        <h4>Access to your information and correction. </h4>
        <p>You have the right to request a copy of the information we hold about you.  If you would like a copy of some or all of the information we hold about you or would like to update your information, please email the site admin or write to us at the address below. </p>
        <h4>Changing your mind.</h4>
        <p>If you  would like to remove your account and delete your data (withdraw your consent), please email msksearch@manchester.ac.uk) or write to us at the address below.</p>
        <p>
            John McBeth <br />
            Arthritis Research UK Centre for Epidemiology <br />
            2nd Floor, Stopford Building<br />
            Oxford Road <br />
            Manchester<br />
            M13 9PT<br />
        </p>

        <h3>Cookies</h3>
        <p>&ldquo;Cookies&rdquo; are small text files that web sites use to store settings and information. The University uses cookies as described in the <a href="https://www.manchester.ac.uk/discover/privacy-information/data-protection/website-privacy-notice/">website privacy notice</a>. IN addition, UK-RiME MSKSearch uses the following cookies to provide you with services:</p>
        <ul><li><strong>.ASPXAUTH</strong>: used to show that you are logged in to the website.</li>
            <li><strong>AcceptCookieNotice</strong>: used to determine whether to show the cookie warning notice at the top of this site.</li>
        </ul>
        <p>For further information on how your data is used, how we maintain the security of your information and your rights to access information we hold on you, please contact <a href="mailto:<%= ConfigurationManager.AppSettings["SupportEmail"] %>">the site administrator</a>. </p>
        <p>If you wish to delete your account at any time, please contact <a href="mailto:<%= ConfigurationManager.AppSettings["SupportEmail"] %>">the site administrator</a>. 
        We will keep your information for a period of up to one year after your account is closed or the system is removed from service, unless you request that your data be deleted.</p>
 
        <h3>Upload Guide</h3>
        <p>When creating  a study profile on our site, you may upload additional files to provide supporting information such as consent forms, patient information sheets and a data access policy. Please note that only blank templates should be uploaded.</p>
        <p>You must not misuse our site by:</p>
        <ol>
            <li>Uploading material for which you do not own the copyright/have appropriate permission from the copyright holder. </li>
            <li>Upoading any personally identifiable information.</li>
            <li>Introducing viruses or other material that is technologically harmful. </li>
        </ol>
 
        <h2>Viruses</h2>
        <p>We do not guarantee that our site will be secure or free from bugs or viruses.   You are responsible for configuring your information technology, computer programmes and platform to access our site. You should use your own virus protection software and check all content that you download from our site before using it.</p>
	<h2>Acknowledgements</h2>
        <p>With thanks to: Professor Wendy Thomson, Professor Will Dixon, Dr Gillian Armitt, Arthritis Research UK, North West eHealth and Jim McGrath (Research Software Engineer). </p>   
        <p>We would also particularly like to thank Lauren Hewitt, Alex Oldroyd and  Dr Jennifer Humphreys for their help developing and reviewing disease area checklists.</p>    
        <h2>Funding</h2>
	<p>This project was supported by a research grant from <a href="http://www.arthritisresearchuk.org/">Arthritis Research UK</a>:</p>
        <img src="/Images/Logos/aruk_logo.png"  alt="aruk logo">
</div>

</asp:Content>