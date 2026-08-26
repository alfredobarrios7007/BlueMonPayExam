package mx.com.invex.passwordrecovery.audit;

import org.apache.logging.log4j.LogManager;
import org.apache.logging.log4j.Logger;

import jakarta.enterprise.context.ApplicationScoped;

@ApplicationScoped
public class MntCustomDataAuditLogger {

    private static final Logger AUDIT_LOG = LogManager.getLogger("mntCustomDataAudit");

    public void logPasswordRecovery(String cui) {
        AUDIT_LOG.info(cui);
    }
}
