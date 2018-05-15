import static com.kms.katalon.core.checkpoint.CheckpointFactory.findCheckpoint
import static com.kms.katalon.core.testcase.TestCaseFactory.findTestCase
import static com.kms.katalon.core.testdata.TestDataFactory.findTestData
import static com.kms.katalon.core.testobject.ObjectRepository.findTestObject
import com.kms.katalon.core.checkpoint.Checkpoint as Checkpoint
import com.kms.katalon.core.checkpoint.CheckpointFactory as CheckpointFactory
import com.kms.katalon.core.mobile.keyword.MobileBuiltInKeywords as MobileBuiltInKeywords
import com.kms.katalon.core.mobile.keyword.MobileBuiltInKeywords as Mobile
import com.kms.katalon.core.model.FailureHandling as FailureHandling
import com.kms.katalon.core.testcase.TestCase as TestCase
import com.kms.katalon.core.testcase.TestCaseFactory as TestCaseFactory
import com.kms.katalon.core.testdata.TestData as TestData
import com.kms.katalon.core.testdata.TestDataFactory as TestDataFactory
import com.kms.katalon.core.testobject.ObjectRepository as ObjectRepository
import com.kms.katalon.core.testobject.TestObject as TestObject
import com.kms.katalon.core.webservice.keyword.WSBuiltInKeywords as WSBuiltInKeywords
import com.kms.katalon.core.webservice.keyword.WSBuiltInKeywords as WS
import com.kms.katalon.core.webui.keyword.WebUiBuiltInKeywords as WebUiBuiltInKeywords
import com.kms.katalon.core.webui.keyword.WebUiBuiltInKeywords as WebUI
import internal.GlobalVariable as GlobalVariable
import org.openqa.selenium.Keys as Keys

WebUI.openBrowser('')

WebUI.navigateToUrl('http://localhost:60564/Account/Login?ReturnUrl=%2fAdmin%2fIndex')

WebUI.setText(findTestObject('Page_ (6)/input_UserName'), 'admin')

WebUI.setText(findTestObject('Page_ (6)/input_Password'), 'secret')

WebUI.click(findTestObject('Page_ (6)/input_btn btn-primary'))

WebUI.click(findTestObject('Page_ (6)/a_Add a new course'))

WebUI.click(findTestObject('Page_ (6)/input_btn btn-primary'))

WebUI.verifyElementText(findTestObject('Validate_Name'), 'Please enter a course name')

WebUI.verifyElementText(findTestObject('Validate_Des'), 'Please enter a description')

WebUI.verifyElementText(findTestObject('Validate_Section'), 'Please enter a section')

WebUI.verifyElementText(findTestObject('Validate_Credit'), 'Please enter a course credit')

WebUI.verifyElementText(findTestObject('Validate_Time'), 'Please enter a course time')

WebUI.verifyElementText(findTestObject('Validate_Day'), 'Please enter day')

WebUI.closeBrowser()

