package mx.com.invex.passwordrecovery;

import static io.restassured.RestAssured.given;
import static org.hamcrest.Matchers.equalTo;

import org.junit.jupiter.api.Test;

import io.quarkus.test.junit.QuarkusTest;

@QuarkusTest
class PasswordRecoveryResourceTest {

    @Test
    void validCredentialsReturnSuccess() {
        given()
                .contentType("application/json")
                .body("""
                        {
                          "data": {
                            "cui": "abc7007",
                            "old-password": "q1w2e3r4",
                            "new-password": "a1s2d3f4",
                            "confirmation": "a1s2d3f4"
                          }
                        }
                        """)
                .when()
                .post("/passwordRecovery")
                .then()
                .statusCode(200)
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@status'", equalTo("000"))
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@statusMsg'", equalTo("success"))
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@cui'", equalTo("abc7007"))
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@version'", equalTo("1.0.0"));
    }

    @Test
    void invalidCredentialsReturnUnauthorized() {
        given()
                .contentType("application/json")
                .body("""
                        {
                          "data": {
                            "cui": "abc7007",
                            "old-password": "wrong",
                            "new-password": "a1s2d3f4",
                            "confirmation": "a1s2d3f4"
                          }
                        }
                        """)
                .when()
                .post("/passwordRecovery")
                .then()
                .statusCode(401)
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@status'", equalTo("401"))
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@statusMsg'", equalTo("wrong password"))
                .body("passwordRecoveryResponse.passwordRecoveryResult.'@cui'", equalTo("abc7007"));
    }
}
